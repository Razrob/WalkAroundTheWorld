using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties : MonoBehaviour
{
    [SerializeField] private bool _isHardSurface;
    [SerializeField] private bool _suitableForTreeSpawn;
    [SerializeField] private int _changeHealthFromTile;

    public bool IsHardSurface => _isHardSurface;
    public bool SuitableForTreeSpawn => _suitableForTreeSpawn;
    public int ChangeHealthFromTile => _changeHealthFromTile;
}
