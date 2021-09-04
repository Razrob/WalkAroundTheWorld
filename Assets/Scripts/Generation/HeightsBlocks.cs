using System;
using UnityEngine;

[Serializable]
public struct HeightsBlocks
{
    public HeightBlock[] Blocks;

    [Serializable]
    public struct HeightBlock
    {
        public GameObject Block;
        public float Height;
    }
}
