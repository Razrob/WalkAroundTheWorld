using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MovingEntity
{

    private FloatingJoystick _joystick;

    protected override void Init()
    {
        _joystick = FindObjectOfType<FloatingJoystick>();
        StartCoroutine(TakeEffects());
    }

    private void Update()
    {
        if (_chunkGenerator.ChunkUpdateCompleted && !_jumping && GetMoveDirection() != Vector3.zero) Move();
    }

    private void Move()
    {
        Action _onMoved = () => _chunkGenerator.UpdateChunks(transform.position);
        _onMoved += TakeItem;

        PlayerStats.MakeStep();
        StartCoroutine(SmoothMove(GetMoveDirection().TransformFromCustomCoordinate(), _onMoved));
    }

    public readonly IReadOnlyList<Vector3> _offcets = new List<Vector3>()
    {
        Vector3.forward,
        Vector3.right,
        Vector3.left,
        Vector3.back,
    };
    private Vector3 GetMoveDirection()
    {
        Vector3 _directionMobile = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);

        Vector3 _directionDesktop = new Vector3();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            _directionDesktop += Vector3.forward;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftAlt))
            _directionDesktop += Vector3.left;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            _directionDesktop += Vector3.back;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            _directionDesktop += Vector3.right;

        _directionDesktop.Normalize();
        _directionMobile.Normalize();

        if (_directionMobile.magnitude < 0.1f && _directionDesktop.magnitude < 0.1f)
            return Vector3.zero;

        _directionMobile = Quaternion.Euler(0, 45, 0) * _directionMobile;
        _directionDesktop = Quaternion.Euler(0, 45, 0) * _directionDesktop;

        Vector3 d;

        if (_directionDesktop.magnitude > 0.1f)
            d = _directionDesktop;
        else
            d = _directionMobile;

        Vector3 coordToTargetOffcet = _offcets.FindMax(o => Vector3.Dot(o, d));

        return coordToTargetOffcet;
        //return Quaternion.Euler(0, 45, 0) * _direction;

        //if (_direction.x != 0 && _direction.y != 0)
        //{
        //    if (_direction.x == _direction.y) _direction = new Vector2Int(_direction.x, 0);
        //    else _direction = new Vector2Int(0, _direction.y);
        //}
        //else _direction = Vector2Int.zero;
        //return new Vector3(_direction.x, 0, _direction.y);

    }

    private void ReadTileProperties(TileProperties _tileProperties)
    {
        if (_tileProperties.ChangeHealthFromTile != 0) PlayerStats.Health += _tileProperties.ChangeHealthFromTile;
        else PlayerStats.Health++;
    }

    private IEnumerator TakeEffects()
    {
        yield return new WaitForSeconds(1);
        TileProperties _tileProperties = _tileGrid.GetTileProperties(transform.position);

        if (_tileProperties != null) ReadTileProperties(_tileProperties);

        StartCoroutine(TakeEffects());
    }

    private void TakeItem()
    {
        ICollectable _item = _tileGrid.TryGetItem(transform.position);
        if (_item != null)
        {
            _item.Collect();
            _tileGrid.ClearItemReference(transform.position);
        }
    }

}
