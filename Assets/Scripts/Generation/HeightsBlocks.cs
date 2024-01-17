using System;
using System.Linq;
using UnityEngine;

[Serializable]
public struct HeightsBlocks
{
    public HeightBlock[] Blocks;

    public void Sort()
    {
        Blocks = Blocks.OrderBy(b => b.Height).ToArray();
    }

    [Serializable]
    public struct HeightBlock
    {
        public GameObject Block;
        public float Height;
    }
}
