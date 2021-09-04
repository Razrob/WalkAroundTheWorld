using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NoiseGenerator
{
    public int OctaveNumber;
    public float Frequency;
    public float FrequencyMultiplier;
    public float Amplitude;
    public float AmplitudeMultiplier;

    public ClampingType ClampingType;

    public NoiseGenerator()
    {
        OctaveNumber = 1;
        Frequency = 1;
        FrequencyMultiplier = 2;
        AmplitudeMultiplier = 0.5f;
        Amplitude = 1;
        ClampingType = ClampingType.Clamp;
    }

    public NoiseGenerator(int _octaveNumber, float _frequency, float _amplitude,  float _frequencyMultiplier, float _amplitudeMultiplier, ClampingType _clampingType)
    {
        OctaveNumber = _octaveNumber;
        Frequency = _frequency;
        Amplitude = _amplitude;
        FrequencyMultiplier = _frequencyMultiplier;
        AmplitudeMultiplier = _amplitudeMultiplier;
        ClampingType = _clampingType;
    }
    public NoiseGenerator(NoiseProperties _properties)
    {
        OctaveNumber = _properties.OctaveNumber;
        Frequency = _properties.Frequency;
        FrequencyMultiplier = _properties.FrequencyMultiplier;
        AmplitudeMultiplier = _properties.AmplitudeMultiplier;
        Amplitude = _properties.Amplitude;
        ClampingType = _properties.ClampingType;
    }

    private float[] GenerateHeights(int _width, int _height, int _seed, Vector2 _offcet = default)
    {
        float[] _heights = new float[_width * _height];

        Random _random = new Random(_seed);

        if(_offcet == default) _offcet = new Vector2(_random.Next(-100000, 100000), _random.Next(-100000, 100000));

        float _maxHeight = (Amplitude * (1 - Mathf.Pow(AmplitudeMultiplier, OctaveNumber))) / (1 - AmplitudeMultiplier);

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                float _resultHeight = 0;

                float _currentFrequency = Frequency;
                float _currentAmplitude = Amplitude;

                for (int i = 0; i < OctaveNumber; i++)
                {
                    float _intermediateHeight = 0;

                    float _xResult = (x + _offcet.x) / (256 / _currentFrequency);
                    float _yResult = (y + _offcet.y) / (256 / _currentFrequency);


                    _intermediateHeight += Mathf.PerlinNoise(_xResult, _yResult);

                    _intermediateHeight -= 0.5f;
                    _intermediateHeight *= 2;

                    _intermediateHeight *= _currentAmplitude;


                    _currentAmplitude *= AmplitudeMultiplier;
                    _currentFrequency *= FrequencyMultiplier;

                    _resultHeight += _intermediateHeight;
                }

                if (ClampingType == ClampingType.Interpolation) _resultHeight /= _maxHeight;
                else if (ClampingType == ClampingType.Clamp) _resultHeight = Mathf.Clamp(_resultHeight, -1, 1);

                _resultHeight /= 2;
                _resultHeight += 0.5f;

                _heights[y * _width + x] = _resultHeight;
            }
        }

        return _heights;
    }
    private Color GetColorFromHeight(float _height, HeightsColors _heightsColors)
    {

        Color _finalColor = _heightsColors.Colors[0].Color;
        for (int i = 0; i < _heightsColors.Colors.Length; i++)
        {
            if (_height <= _heightsColors.Colors[i].Height) break;
            else if (_heightsColors.Colors.Length > i + 1) _finalColor = _heightsColors.Colors[i + 1].Color;
        }
        return _finalColor;
    }





    public Texture2D GetRawTexture(int _textureWidth, int _textureHeight, int _seed)
    {
        Texture2D _texture = new Texture2D(_textureWidth, _textureHeight);

        float[] _heights = GenerateHeights(_textureWidth, _textureHeight, _seed);

        for (int y = 0; y < _textureHeight; y++)
        {
            for (int x = 0; x < _textureWidth; x++)
            {
                _texture.SetPixel(x, y, new Color(_heights[y * _textureWidth + x], _heights[y * _textureWidth + x], _heights[y * _textureWidth + x]));
            }
        }

        _texture.Apply();

        return _texture;
    }

    public Texture2D GetColorTexture(int _textureWidth, int _textureHeight, int _seed, Gradient _gradient)
    {
        Texture2D _texture = new Texture2D(_textureWidth, _textureHeight);

        float[] _heights = GenerateHeights(_textureWidth, _textureHeight, _seed);

        for (int y = 0; y < _textureHeight; y++)
        {
            for (int x = 0; x < _textureWidth; x++)
            {
                _texture.SetPixel(x, y, _gradient.Evaluate(_heights[y * _textureWidth + x]));
            }
        }

        _texture.Apply();

        return _texture;
    }

    public Texture2D GetColorTexture(int _textureWidth, int _textureHeight, int _seed, HeightsColors _heightsColors)
    {
        Texture2D _texture = new Texture2D(_textureWidth, _textureHeight);

        float[] _heights = GenerateHeights(_textureWidth, _textureHeight, _seed);

        for (int y = 0; y < _textureHeight; y++)
        {
            for (int x = 0; x < _textureWidth; x++)
            {
                _texture.SetPixel(x, y, GetColorFromHeight(_heights[y * _textureWidth + x], _heightsColors));
            }
        }

        _texture.Apply();

        return _texture;
    }

    public TerrainHeights GetTerrainHeights(int _terrainWidth, int _terrainHeight, int _seed)
    {
        TerrainHeights _terrainHeights = new TerrainHeights(_terrainWidth, _terrainHeight);

        float[] _heights = GenerateHeights(_terrainWidth, _terrainHeight, _seed);


        for (int y = 0; y < _terrainHeight; y++)
        {
            for (int x = 0; x < _terrainWidth; x++)
            {
                _terrainHeights[x, y] = _heights[y * _terrainWidth + x];
            }
        }

        return _terrainHeights;
    }
    public TerrainHeights GetTerrainHeights(int _terrainWidth, int _terrainHeight, Vector2 _offcet)
    {
        TerrainHeights _terrainHeights = new TerrainHeights(_terrainWidth, _terrainHeight);

        float[] _heights = GenerateHeights(_terrainWidth, _terrainHeight, 0, _offcet);


        for (int y = 0; y < _terrainHeight; y++)
        {
            for (int x = 0; x < _terrainWidth; x++)
            {
                _terrainHeights[x, y] = _heights[y * _terrainWidth + x];
            }
        }

        return _terrainHeights;
    }
}

