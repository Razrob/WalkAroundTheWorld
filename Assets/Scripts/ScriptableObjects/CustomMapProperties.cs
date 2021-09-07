using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMapProperties : MapProperties
{
    public HeightsBlocks SetHeightsBlocks { set { _heightsBlocks = value; } }
    public NoiseProperties SetNoiseProperties { set { _noiseProperties = value; } }
    public float SetVerticalScale { set { _verticalScale = value; } }
    public float SetWaterLevel { set { _waterLevel = value; } }
    public string SetName { set { _mapName = value; } }
    public Sprite SetSprite { set { _mapImage = value; } }

}
