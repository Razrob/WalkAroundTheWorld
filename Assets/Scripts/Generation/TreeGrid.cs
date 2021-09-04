using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrid
{
    private Dictionary<Vector2Int, Tree> _trees = new Dictionary<Vector2Int, Tree>();


    public void AddTree(Vector2Int _position, Tree _tree)
    {
        if (_trees.ContainsKey(_position)) _trees[_position] = _tree;
        else _trees.Add(_position, _tree);
    }

    public Tree GetTree(Vector2Int _position)
    {
        if (_trees.ContainsKey(_position)) return _trees[_position];
        return null;
    }

    public void RemoveTree(Vector2Int _position)
    {
        if (_trees.ContainsKey(_position)) _trees.Remove(_position);
    }

    public bool TreeSpawnAvailable(Vector2Int _position, float _treeRadius)
    {
        if (_trees.ContainsKey(_position)) return false;
        foreach(KeyValuePair<Vector2Int, Tree> _tree in _trees) if (Vector2Int.Distance(_tree.Key, _position) <= _tree.Value.Radius + _treeRadius) return false;
        return true;
    }

}
