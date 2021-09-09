using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMapProperties : MapProperties
{
    public HeightsBlocks CustomHeightsBlocks { set { _heightsBlocks = value; } }
    public NoiseProperties CustomNoiseProperties { set { _noiseProperties = value; } }
    public float CustomVerticalScale { set { _verticalScale = value; } }
    public float CustomWaterLevel { set { _waterLevel = value; } }
    public string CustomName { set { _mapName = value; } }
    public Sprite CustomSprite { set { _mapImage = value; } }

}
