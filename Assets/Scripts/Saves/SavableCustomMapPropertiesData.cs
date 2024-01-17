using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SavableCustomMapPropertiesData 
{
    public MapData[] CustomMapProperetiesData;
    public string SelectedMapName;

    [Serializable]
    public class MapData
    {
        public NoiseProperties NoiseProperties;
        public HeightsBlocksData HeightsBlocksData;

        public float VerticalScale;
        public float WaterLevel;

        public string MapName;
        public string SpriteName;
    }

    [Serializable]
    public struct HeightsBlocksData
    {
        public StringFloatPair[] Blocks;
    }
}

[Serializable]
public struct StringFloatPair
{
    public string Value1;
    public float Value2;

    public StringFloatPair(string value1, float value2)
    {
        Value1 = value1;
        Value2 = value2;
    }
}