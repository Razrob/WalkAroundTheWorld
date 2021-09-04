using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    public float SpawnChance { get; }
    public float SpawnHeightOffcet { get; }
}
