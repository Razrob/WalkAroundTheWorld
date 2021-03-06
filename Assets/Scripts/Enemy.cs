using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MovingEntity
{
    [SerializeField] private float _jumpsPerSecond;
    [SerializeField] private int _damagePerAttack;
    [SerializeField] private float _maxDistance;

    [HideInInspector] public Transform Target;

    protected override void Init()
    {
        if (_jumpsPerSecond <= 0) _jumpsPerSecond = 1;

        _jumpsPerSecond = UnityEngine.Random.Range(_jumpsPerSecond - 0.5f, _jumpsPerSecond + 0.5f);
        _damagePerAttack = UnityEngine.Random.Range(_damagePerAttack - 5, _damagePerAttack + 5);
        _maxDistance = UnityEngine.Random.Range(_maxDistance - 5, _maxDistance + 5);
        _jumpHeight = UnityEngine.Random.Range(_jumpHeight - 1f, _jumpHeight + 2f);

        transform.localScale = Vector3.one * UnityEngine.Random.Range(0.5f, 1f);
        _tileUpOffcet = transform.localScale.x * 2;
        _shadowScale = 2 * transform.localScale.x;
        _shadowOffcet = new Vector3(0, -transform.localScale.x * 2 + 0.07f, 0.025f);

        StartCoroutine(Move());
    }

    private void DestroyEnemy()
    {
        Destroy(_shadow.gameObject);
        Destroy(gameObject);
    }

    private bool CheckContactingToPlayer()
    {
        return new Vector2(transform.position.x, transform.position.z).TransformToCustomCoordinate() 
            == new Vector2(Target.position.x, Target.position.z).TransformToCustomCoordinate();
    }

    private void Attack(Vector2Int _direction)
    {
        PlayerStats.Health -= _damagePerAttack;

        StartCoroutine(SmoothMove(new Vector3(-_direction.x, 0, -_direction.y).TransformFromCustomCoordinate()));
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(1 / _jumpsPerSecond);

        while (_jumping || Target == null) yield return null;

        if (!_tileGrid.CheckTileAvailability(transform.position)) DestroyEnemy();
        else
        {

            Vector2Int _position = new Vector2(transform.position.x, transform.position.z).TransformToCustomCoordinate();
            Vector2Int _playerPosition = new Vector2(Target.position.x, Target.position.z).TransformToCustomCoordinate();
            
            Vector2Int _direction = _playerPosition - _position;

            if (_direction.magnitude > _maxDistance)
            {
                StartCoroutine(Move());
                yield break;
            }

            _direction = _direction.Normalize();

            if (_direction.x != 0 && _direction.y != 0)
            {
                if (UnityEngine.Random.Range(0f, 1f) >= 0.5f) _direction = new Vector2Int(_direction.x, 0);
                else _direction = new Vector2Int(0, _direction.y);
            }

            if (!_tileGrid.CheckTileAvailability(transform.position + new Vector3(_direction.x, 0, _direction.y).TransformFromCustomCoordinate())) DestroyEnemy();
            else
            {
                Action _onMoved = () => { if (CheckContactingToPlayer()) Attack(_direction); };
                _onMoved += () => { if (!_tileGrid.CheckTileAvailability(transform.position)) DestroyEnemy(); };

                StartCoroutine(SmoothMove(new Vector3(_direction.x, 0, _direction.y).TransformFromCustomCoordinate(), _onMoved));

                StartCoroutine(Move());
            }
        }

    }
}
