using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SavableMapPropertiesData
{
    public List<string> MapPropertiesFileNames = new List<string>();
    public string SelectedMapPropertiesName;
}