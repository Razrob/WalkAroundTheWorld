using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private TileGrid _tileGrid;
    [SerializeField] private Transform _enemyTarget;

    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private float _enemyDensity;

    private void Start()
    {
        _tileGrid.OnCellAdded += SpawnEnemy;
        _enemyTarget = FindObjectOfType<Player>().transform;
    }

    private void SpawnEnemy(Vector2Int _cellPosition)
    {
        if (!_tileGrid.GetCell(_cellPosition).TileProperties.IsHardSurface) return;

        if (Random.Range(0f, 1f) <= _enemyDensity)
        {

            Instantiate(_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)], new Vector3(_cellPosition.x, 0, _cellPosition.y).TransformFromCustomCoordinate(), Quaternion.identity).Target = _enemyTarget;

        }
    }

}
