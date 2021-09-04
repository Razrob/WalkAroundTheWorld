using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileGrid : MonoBehaviour
{
    private Dictionary<Vector2Int, TileCell> _tileCells;

    public event Action<Vector2Int> OnCellAdded;
    public event Action<Vector2Int> OnCellRemoved;

    public TileGrid()
    {
        _tileCells = new Dictionary<Vector2Int, TileCell>();
    }

    public TileCell this[Vector2Int _key]
    {
        get
        {
            if(_tileCells.ContainsKey(_key)) return _tileCells[_key];
            return null;
        }
    }

    public bool CellIsExists(Vector2Int _position)
    {
        return _tileCells.ContainsKey(_position);
    }

    public void AddCell(Vector2Int _position, TileCell _cell)
    {
        if (!_tileCells.ContainsKey(_position)) _tileCells.Add(_position, _cell);
        else _tileCells[_position] = _cell;
        OnCellAdded?.Invoke(_position);
    }

    public void RemoveCell(Vector2Int _position)
    {
        if (_tileCells.ContainsKey(_position)) _tileCells.Remove(_position);
        OnCellRemoved?.Invoke(_position);
    }

    public TileCell GetCell(Vector2Int _position)
    {
        if (_tileCells.ContainsKey(_position)) return _tileCells[_position];
        return null;
    }

    public void AddItem(Vector2Int _position, ICollectable _item)
    {
        if (_tileCells.ContainsKey(_position)) _tileCells[_position]._item = _item;
    }




    public float GetTileHeight(Vector3 _position)
    {
        Vector2Int _playerPosition = new Vector2(_position.x, _position.z).TransformToCustomCoordinate();
        if (_tileCells[_playerPosition] != null)
        {
            float _height = _tileCells[_playerPosition].Y;
            return new Vector3(_playerPosition.x, _height, _playerPosition.y).TransformFromCustomCoordinate().y;
        }
        return float.NaN;
    }
    public TileProperties GetTileProperties(Vector3 _position)
    {
        Vector2Int _playerPosition = new Vector2(_position.x, _position.z).TransformToCustomCoordinate();
        if (_tileCells[_playerPosition] != null) return _tileCells[_playerPosition].TileProperties;
        return null;
    }
    public ICollectable TryGetItem(Vector3 _position)
    {
        Vector2Int _playerPosition = new Vector2(_position.x, _position.z).TransformToCustomCoordinate();
        return GetCell(_playerPosition)?._item;
    }
    public void ClearItemReference(Vector3 _position)
    {
        Vector2Int _playerPosition = new Vector2(_position.x, _position.z).TransformToCustomCoordinate();
        GetCell(_playerPosition)._item = null;
    }

    public bool CheckTileAvailability(Vector3 _position)
    {
        Vector2Int _playerPosition = new Vector2(_position.x, _position.z).TransformToCustomCoordinate();
        return GetCell(_playerPosition) != null;
    }

    public void AddTreeToCell(Vector2Int _position)
    {
        TileCell _tileCell = GetCell(_position);
        if (_tileCell != null) _tileCell.TileIsBusy = true;
    }

    public bool MoveToCellAvailable(Vector3 _position)
    {
        Vector2Int _playerPosition = new Vector2(_position.x, _position.z).TransformToCustomCoordinate();
        TileCell _tileCell = GetCell(_playerPosition);

        return _tileCell != null &&
            _tileCell.TileProperties.IsHardSurface &&
            !_tileCell.TileIsBusy;
    }
}
