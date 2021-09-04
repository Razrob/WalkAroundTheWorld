using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MapShopDisplay : PageShopDisplay
{
    [SerializeField] private MapProperties[] _mapItems;


    private void OnEnable()
    {
        SetSelectedMap(0);
    }

    protected override void SelectItem()
    {
        AvailabilityMapProperties.SelectedMap = _mapItems[_selectedItemIndex];
        UpdateItemDisplay();
    }

    protected override bool ItemIsSelect() => _mapItems[_selectedItemIndex] == AvailabilityMapProperties.SelectedMap;

    protected override void Init()
    {
        _items = _mapItems;
    }

    protected override bool CheckPurchased()
    {
        for (int i = 0; i < AvailabilityMapProperties.MapCount; i++) if (AvailabilityMapProperties.GetMapProperties(i) == _mapItems[_selectedItemIndex]) return true;
        return false;
    }

    public override void BuySelectedMap()
    {
        if (CoinWallet.TryMakePurchase(_mapItems[_selectedItemIndex].ItemPrice))
        {
            AvailabilityMapProperties.AddMapProperties(_mapItems[_selectedItemIndex]);
            SelectItem();
            UpdateItemDisplay();
        }
    }

}
