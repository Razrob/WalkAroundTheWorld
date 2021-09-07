using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private TileGrid _tileGrid;

    [SerializeField] private Tree[] _trees;

    private TreeGrid _treeGrid = new TreeGrid();

    private void Start()
    {
        _tileGrid.OnCellAdded += SpawnTree;
        _tileGrid.OnCellRemoved += RemoveTree;
    }
    
    private void SpawnTree(Vector2Int _position)
    {
        if (!_tileGrid.GetCell(_position).TileProperties.SuitableForTreeSpawn || _tileGrid.GetCell(_position)._item != null) return;

        int _treeIndex = Random.Range(0, _trees.Length);

        if(Random.Range(0f, 1f) < _trees[_treeIndex].SpawnChance * _trees.Length)
        {
            if(_treeGrid.TreeSpawnAvailable(_position, _trees[_treeIndex].Radius))
            {
                Vector3 _pos = new Vector3(_position.x, _tileGrid.GetCell(_position).Y, _position.y).TransformFromCustomCoordinate();
                _treeGrid.AddTree(_position, Instantiate(_trees[_treeIndex], _pos + Vector3.back * 0.05f, Quaternion.identity));
                _tileGrid.AddTreeToCell(_position);
            }
        }
    }

    private void RemoveTree(Vector2Int _position)
    {
        Tree _tree = _treeGrid.GetTree(_position);
        if(_tree != null)
        {
            _treeGrid.RemoveTree(_position);
            Destroy(_tree.gameObject);
        }
    }

}


