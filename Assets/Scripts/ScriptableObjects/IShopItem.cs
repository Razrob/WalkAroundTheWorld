using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IShopItem
{
    public Sprite ItemImage { get; }
    public int ItemPrice { get; }
    public string ItemName { get; }
}
