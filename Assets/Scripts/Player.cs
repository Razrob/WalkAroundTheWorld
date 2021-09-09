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

    private Vector3 GetMoveDirection()
    {
        Vector2Int _direction = new Vector2Int(Convert.ToInt32(_joystick.Horizontal * 3f), Convert.ToInt32(_joystick.Vertical * 3f));
        _direction = _direction.Normalize();

        if (_direction.x != 0 && _direction.y != 0)
        {
            if (_direction.x == _direction.y) _direction = new Vector2Int(_direction.x, 0);
            else _direction = new Vector2Int(0, _direction.y);
        }
        else _direction = Vector2Int.zero;
        return new Vector3(_direction.x, 0, _direction.y);

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
