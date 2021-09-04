using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _spawnChance;

    public float Radius => _radius;
    public float SpawnChance => _spawnChance;
}
