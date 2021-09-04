using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileWorldGenerator : MonoBehaviour
{
    [SerializeField] private string _spritePath;
    [SerializeField] private string _filePath;
    [SerializeField] private string _fileName;
    [SerializeField] private int _itemPrice;

    [Header("Main")]
    [SerializeField] private Vector2 _tileOffcets;
    [SerializeField] private Vector2Int _worldSize;
    [SerializeField] private HeightsBlocks _heightsBlocks;
    [SerializeField] private float _verticalScale;
    [SerializeField] private float _waterLevel;

    [Header("Generation properties")]
    [SerializeField] private int _seed = 1;
    [SerializeField] private int _octaveNumber = 1;
    [SerializeField] private float _frequency = 1;
    [SerializeField] private float _frequencyMultiplier = 2;
    [SerializeField] private float _amplitude = 1;
    [SerializeField] private float _amplitudeMultiplier = 0.5f;
    [SerializeField] private ClampingType _clampingType = ClampingType.Clamp;


    private void Start()
    {

        NoiseGenerator _noiseGenerator = new NoiseGenerator
        {
            OctaveNumber = _octaveNumber,
            Frequency = _frequency,
            FrequencyMultiplier = _frequencyMultiplier,
            Amplitude = _amplitude,
            AmplitudeMultiplier = _amplitudeMultiplier,
            ClampingType = _clampingType
        };

        TerrainHeights _heights = _noiseGenerator.GetTerrainHeights(_worldSize.x, _worldSize.y, _seed);


        for (int x = 0; x < _worldSize.x; x++)
        {
            for (int y = 0; y < _worldSize.y; y++)
            {
                Vector3 _position = new Vector3(x - y, x / 2f + y / 2f, x / 2f + y / 2f);

                _position.y += _heights[x, y] * _verticalScale;
                if (_heights[x, y] < _waterLevel) _position.y = _waterLevel * _verticalScale + x / 2f + y / 2f;
                Instantiate(GetBlockFromHeight(_heights[x, y]), _position, Quaternion.identity);
            }
        }

        ScreenCapture.CaptureScreenshot($"{_spritePath}/{_fileName}.png");

        MapProperties _mapProperties = ScriptableObject.CreateInstance<MapProperties>();

        _mapProperties._heightsBlocks = _heightsBlocks;
        _mapProperties._noiseProperties = new NoiseProperties
        {
            OctaveNumber = _octaveNumber,
            Frequency = _frequency,
            FrequencyMultiplier = _frequencyMultiplier,
            Amplitude = _amplitude,
            AmplitudeMultiplier = _amplitudeMultiplier,
            ClampingType = _clampingType
        };
        _mapProperties._verticalScale = _verticalScale;
        _mapProperties._waterLevel = _waterLevel;

        _mapProperties._mapPrice = _itemPrice;

        //Debug.Log(Application.dataPath + "/" + _filePath + "/" + _fileName + ".asset");
        
        AssetDatabase.CreateAsset(_mapProperties, "Assets/" + _filePath + "/" + _fileName + ".asset");
        AssetDatabase.SaveAssets();

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

}
