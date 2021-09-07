using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private MapProperties _tempMapProperties;

    [SerializeField] private TileGrid _tileGrid;

    [SerializeField] private int _chunkNumber;
    [SerializeField] private int _chunkSize;

    private HeightsBlocks _heightsBlocks;

    private float _verticalScale;
    private float _waterLevel;

    private int _seed;

    private NoiseGenerator _noiseGenerator;
    private MapProperties _mapProperties = AvailabilityMapProperties.SelectedMap;

    private Vector2Int _lastMinChunkPosition;
    private Vector2Int _globalOffcet;

    

    private void Awake()
    {
         _seed = UnityEngine.Random.Range(0, 900000000);

        _noiseGenerator = new NoiseGenerator(_mapProperties.NoiseProperties);
        _verticalScale = _mapProperties.VerticalScale;
        _waterLevel = _mapProperties.WaterLevel;
        _heightsBlocks = _mapProperties.HeightsBlocks;

        System.Random _random = new System.Random(_seed);

        bool _isCorrect = false;
        for (int i = 0; i < 1000; i++)
        {
            if (_isCorrect) break;
            _globalOffcet = new Vector2Int(_random.Next(-10000000, 10000000), _random.Next(-10000000, 1000000));
            TerrainHeights _heights = _noiseGenerator.GetTerrainHeights(1, 1, Vector2.zero + _globalOffcet);
            if (GetBlockFromHeight(_heights[0, 0]).GetComponent<TileProperties>().IsHardSurface) _isCorrect = true;
        }

        UpdateChunks(Vector3.zero);

    }
    private void DestroyUnusedChunks(Vector2Int _minChunkPosition)
    {
        if (_minChunkPosition == _lastMinChunkPosition) return;
        Vector2Int _offcet = _lastMinChunkPosition - _minChunkPosition;
        _offcet *= _chunkNumber + 1;

        Vector2Int _minPosition = new Vector2Int(_minChunkPosition.x + _offcet.x, _minChunkPosition.y + _offcet.y);

        int _zLength;
        int _xLength;

        if (_offcet.x != 0)
        {
            _minPosition.y -= _chunkSize * _chunkNumber;
            _zLength = _minPosition.y + _chunkSize * (_chunkNumber * 2 + 1);
            _xLength = _minPosition.x + _chunkSize;
        }
        else
        {
            _minPosition.x -= _chunkSize * _chunkNumber;
            _zLength = _minPosition.y + _chunkSize;
            _xLength = _minPosition.x + _chunkSize * (_chunkNumber * 2 + 1);
        }

        for (int z = _minPosition.y; z < _zLength; z++)
        {
            for (int x = _minPosition.x; x < _xLength; x++)
            {
                if (!_tileGrid.CellIsExists(new Vector2Int(x, z))) continue;
                TileCell _cell = _tileGrid.GetCell(new Vector2Int(x, z));
                Destroy(_cell.TileProperties.gameObject);
                _cell._item?.Destroy();
                _tileGrid.RemoveCell(new Vector2Int(x, z));
            }
        }

        _lastMinChunkPosition = _minChunkPosition;
    }

    private Vector2Int[] GetRequiredChunksPositions(Vector2Int _minChunkPosition)
    {
        Vector2Int[] _requiredChunksPositions = new Vector2Int[Convert.ToInt32(Mathf.Pow(_chunkNumber * 2 + 1, 2))];

        int _lineLength = Convert.ToInt32(Mathf.Sqrt(_requiredChunksPositions.Length));
        Vector2Int _firstChunkPosition = _minChunkPosition - new Vector2Int(_chunkSize * _chunkNumber, _chunkSize * _chunkNumber);
        for (int y = 0; y < _lineLength; y++)
        {
            for (int x = 0; x < _lineLength; x++)
            {
                _requiredChunksPositions[y * _lineLength + x] = _firstChunkPosition + new Vector2Int(x * _chunkSize, y * _chunkSize);
            }
        }
        return _requiredChunksPositions;
    }
    private GameObject GetBlockFromHeight(float _height)
    {
        GameObject _block = _heightsBlocks.Blocks[0].Block;
        for (int i = 0; i < _heightsBlocks.Blocks.Length; i++)
        {
            if (_height <= _heightsBlocks.Blocks[i].Height) break;
            else if (_heightsBlocks.Blocks.Length > i + 1) _block = _heightsBlocks.Blocks[i + 1].Block;

        }
        return _block;
    }

    public void UpdateChunks(Vector3 _position)
    {

        Vector2Int _playerPosition = new Vector2(_position.x, _position.z).TransformToCustomCoordinate();

        Vector2Int _minChunkPosition = new Vector2Int();


        if (_playerPosition.x >= 0) _minChunkPosition.x = _playerPosition.x - Math.Abs(_playerPosition.x % _chunkSize);
        else if (_playerPosition.x % _chunkSize == 0) _minChunkPosition.x = _playerPosition.x;
        else _minChunkPosition.x = _playerPosition.x - (_chunkSize + _playerPosition.x % _chunkSize);

        if (_playerPosition.y >= 0) _minChunkPosition.y = _playerPosition.y - Math.Abs(_playerPosition.y % _chunkSize);
        else if (_playerPosition.y % _chunkSize == 0) _minChunkPosition.y = _playerPosition.y;
        else _minChunkPosition.y = _playerPosition.y - (_chunkSize + _playerPosition.y % _chunkSize);

        DestroyUnusedChunks(_minChunkPosition);

        Vector2Int[] _requiredChunksPositions = GetRequiredChunksPositions(_minChunkPosition);

        for(int i = 0; i < _requiredChunksPositions.Length; i++)
        {
            if (_tileGrid.CellIsExists(_requiredChunksPositions[i])) continue;
            TerrainHeights _heights = _noiseGenerator.GetTerrainHeights(_chunkSize, _chunkSize, _requiredChunksPositions[i] + _globalOffcet);

            for(int z = _requiredChunksPositions[i].y; z < _requiredChunksPositions[i].y + _chunkSize; z++)
            {
                for (int x = _requiredChunksPositions[i].x; x < _requiredChunksPositions[i].x + _chunkSize; x++)
                {
                    int _xIndex = Mathf.Abs(_requiredChunksPositions[i].x - x);
                    int _zIndex = Mathf.Abs(_requiredChunksPositions[i].y - z);

                    Vector3 _tilePosition = new Vector3(x, _heights[_xIndex, _zIndex] * _verticalScale, z);

                    if (_heights[_xIndex, _zIndex] < _waterLevel) _tilePosition.y = _waterLevel * _verticalScale;

                    TileProperties _tileProperties = Instantiate(GetBlockFromHeight(_heights[_xIndex, _zIndex]), _tilePosition.TransformFromCustomCoordinate(), Quaternion.identity).GetComponent<TileProperties>();

                    _tileGrid.AddCell(new Vector2Int(x, z), new TileCell { Y = _tilePosition.y, IsExists = true, TileProperties = _tileProperties });
                }
            }

        }
    }
}
