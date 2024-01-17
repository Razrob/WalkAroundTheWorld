using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10000)]
public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private TileGrid _tileGrid;
    [SerializeField] private GameObject[] _items;

    private void Awake()
    {
        _tileGrid.OnCellAdded += SpawnItem;
    }

    private void SpawnItem(Vector2Int _cellPosition)
    {
        if (!_tileGrid.GetCell(_cellPosition).TileProperties.IsHardSurface || _tileGrid.GetCell(_cellPosition).TileIsBusy) return;

        int _itemIndex = Random.Range(0, _items.Length);
        ISpawnable _spawnable = _items[_itemIndex].GetComponent<ISpawnable>();

        if (Random.Range(0f, 1f) < _spawnable.SpawnChance * _items.Length)
        {
            Vector3 _position = new Vector3(_cellPosition.x, _tileGrid.GetCell(_cellPosition).Y, _cellPosition.y);
            ICollectable _item = Instantiate(_items[_itemIndex], _position.TransformFromCustomCoordinate() + Vector3.up * _spawnable.SpawnHeightOffcet + Vector3.back * 0.1f, Quaternion.identity).GetComponent<ICollectable>();
            _tileGrid.AddItem(_cellPosition, _item);
        }
    }

}
