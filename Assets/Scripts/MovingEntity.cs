using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingEntity : MonoBehaviour
{
    [SerializeField] private bool _is3DModel;
    [SerializeField] private float _offcetZ;
    [SerializeField] protected float _tileUpOffcet;
    [SerializeField] private float _maxJumpHeight;
    [SerializeField] protected float _jumpHeight;
    [SerializeField] protected int _jumpDistance;

    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private AnimationCurve _CurveX;
    [SerializeField] private AnimationCurve _CurveZ;
    [SerializeField] private AnimationCurve _playerScaleCurve;
    [SerializeField] private AnimationCurve _shadowScaleCurve;

     protected ChunkGenerator _chunkGenerator;
     protected TileGrid _tileGrid;

    [SerializeField] protected Transform _shadowPrefab;
    [SerializeField] protected float _shadowScale;
    [SerializeField] protected Vector3 _shadowOffcet;

    protected Transform _shadow;
    protected bool _jumping;

    private Vector3 _startScale;

    private void Start()
    {

        _jumpHeight *= _jumpDistance;

        _shadow = Instantiate(_shadowPrefab, transform.position + _shadowOffcet, Quaternion.identity);
        _shadow.localScale = Vector3.one * _shadowScale;
        _chunkGenerator = FindObjectOfType<ChunkGenerator>();
        _tileGrid = FindObjectOfType<TileGrid>();

        RefreshPosition();

        Init();
        _startScale = transform.localScale;

    }

    protected virtual void Init() { }

    private void RefreshPosition()
    {
        if (!_tileGrid.CheckTileAvailability(transform.position)) return;
        Vector3 _position;
        _position.x = Convert.ToInt32(transform.position.x);
        _position.z = Convert.ToInt32(transform.position.z) + _offcetZ;
        _position.y = _tileGrid.GetTileHeight(transform.position) + _tileUpOffcet;
        transform.position = _position;
        _shadow.position = transform.position + _shadowOffcet;
    }

    protected IEnumerator SmoothMove(Vector3 _move, Action _onMoved = null)
    {
        while (_jumping) yield return null;
        _jumping = true;
        _move *= _jumpDistance;

        Vector3 _startPosition = transform.position;
        Vector3 _finalPosition = transform.position + _move;
        _finalPosition.y = _tileGrid.GetTileHeight(_finalPosition) + _tileUpOffcet;

        Quaternion _finalRotation = Quaternion.LookRotation(-_move, new Vector3(0, 17.93353f, -8.850432f));

        if (_tileGrid.MoveToCellAvailable(_finalPosition))
        {
            if (_finalPosition.y < _startPosition.y || Mathf.Abs(_startPosition.y - _finalPosition.y) <= _maxJumpHeight + 0.5f)
            {
                int _iterationNumber = 10 * _jumpDistance * 2;
                for (int i = 1; i <= _iterationNumber; i++)
                {
                    float t = i * 1f / _iterationNumber;

                    Vector3 _offcet;
                    _offcet.x = _startPosition.x + (_finalPosition.x - _startPosition.x) * _CurveX.Evaluate(t);
                    _offcet.y = _startPosition.y + (_finalPosition.y - _startPosition.y) * t + _jumpCurve.Evaluate(t) * _jumpHeight;

                    if (_finalPosition.z - _startPosition.z < 0) _offcet.z = _startPosition.z + (_finalPosition.z - _startPosition.z) * _CurveZ.Evaluate(t);
                    else _offcet.z = _startPosition.z + (_finalPosition.z - _startPosition.z) * _CurveZ.Evaluate(t - 0.5f);


                    transform.position = _offcet;

                    _shadow.position = new Vector3(_offcet.x + _shadowOffcet.x, _startPosition.y + (_finalPosition.y - _startPosition.y) * t + _shadowOffcet.y, _offcet.z + _shadowOffcet.z);
                    _shadow.localScale = Vector3.one * _shadowScale * _shadowScaleCurve.Evaluate(t);
                    transform.localScale = _startScale * _playerScaleCurve.Evaluate(t);

                    if (_is3DModel) transform.rotation = Quaternion.RotateTowards(transform.rotation, _finalRotation, 180f / _iterationNumber);

                    yield return new WaitForSeconds(0.01f / 2);
                }

                if (_is3DModel) transform.rotation = _finalRotation;

                _onMoved?.Invoke();

                RefreshPosition();
            }
        }

        _jumping = false;
    }
}
