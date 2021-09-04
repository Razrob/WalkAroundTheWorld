using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MovingEntity
{
    protected override void Init()
    {
        StartCoroutine(TakeEffects());
    }

    private void Update()
    {
        if (!_jumping) Move();
    }

    private void Move()
    {
        Vector2Int _move = new Vector2Int();
        if (Input.GetAxisRaw("Horizontal") != 0) _move.x = (int)Input.GetAxisRaw("Horizontal");
        else if (Input.GetAxisRaw("Vertical") != 0) _move.y = (int)Input.GetAxisRaw("Vertical");
        else return;

        Action _onMoved = () => _chunkGenerator.UpdateChunks(transform.position);
        _onMoved += TakeItem;

        PlayerStats.MakeStep();
        StartCoroutine(SmoothMove(new Vector3(_move.x, 0, _move.y).TransformFromCustomCoordinate(), _onMoved)); 
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
