using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SavableCustomMapPropertiesData 
{
    public MapData[] CustomMapProperetiesData;

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
        public (string, float)[] Blocks;
    }
}
