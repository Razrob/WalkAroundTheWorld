using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct HeightsColors
{
    public HeightColor[] Colors;

    [Serializable]
    public struct HeightColor
    {
        public Color Color;
        public float Height;
    }
}
