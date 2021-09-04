using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Create map property", order = 1)]
public class MapProperties : ScriptableObject, IShopItem
{
    [Header("For game")]
    [SerializeField] public HeightsBlocks _heightsBlocks;
    [SerializeField] public NoiseProperties _noiseProperties;

    [SerializeField] public float _verticalScale;
    [SerializeField] public float _waterLevel;

    [Header("For shop")]
    [SerializeField] public Sprite _mapImage;
    [SerializeField] public int _mapPrice;

    public HeightsBlocks HeightsBlocks => _heightsBlocks;
    public NoiseProperties NoiseProperties => _noiseProperties;
    public float VerticalScale => _verticalScale;
    public float WaterLevel => _waterLevel;

    public Sprite ItemImage => _mapImage;
    public int ItemPrice => _mapPrice;

}
