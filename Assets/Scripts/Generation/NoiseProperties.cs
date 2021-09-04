using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct NoiseProperties
{
    public int OctaveNumber;
    public float Frequency;
    public float FrequencyMultiplier;
    public float Amplitude;
    public float AmplitudeMultiplier;

    public ClampingType ClampingType;
}
