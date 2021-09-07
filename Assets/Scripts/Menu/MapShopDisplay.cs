using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MapShopDisplay : PageShopDisplay
{
    [SerializeField] private MapProperties[] _mapItems;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        SetSelectedItem(0);
    }

    protected override void SelectItem()
    {
        if (_selectedItemIndex < _mapItems.Length) AvailabilityMapProperties.SetSelectedMap(_mapItems[_selectedItemIndex]);
        else AvailabilityMapProperties.SetSelectedMap(AvailabilityMapProperties.GetCustomMapProperties(_selectedItemIndex - _mapItems.Length));
        UpdateItemDisplay();
    }

    protected override bool ItemIsSelect()
    {
        if (_selectedItemIndex < _mapItems.Length) return _mapItems[_selectedItemIndex] == AvailabilityMapProperties.SelectedMap;
        else return AvailabilityMapProperties.GetCustomMapProperties(_selectedItemIndex - _mapItems.Length) == AvailabilityMapProperties.SelectedMap;
    }

    protected void Init()
    {
        _items = new IShopItem[_mapItems.Length + AvailabilityMapProperties.CustomMapCount];
        for (int i = 0; i < _mapItems.Length; i++) _items[i] = _mapItems[i];
        for (int i = 0; i < AvailabilityMapProperties.CustomMapCount; i++) _items[_mapItems.Length + i] = AvailabilityMapProperties.GetCustomMapProperties(i);
    }

    protected override bool CheckPurchased()
    {
        if (_selectedItemIndex >= _mapItems.Length) return true;
        for (int i = 0; i < AvailabilityMapProperties.MapCount; i++) if (AvailabilityMapProperties.GetMapProperties(i) == _mapItems[_selectedItemIndex]) return true;
        return false;
    }

    public override void BuySelectedItem()
    {
        if (CoinWallet.TryMakePurchase(_mapItems[_selectedItemIndex].ItemPrice))
        {
            AvailabilityMapProperties.AddMapProperties(_mapItems[_selectedItemIndex]);
            SelectItem();
            UpdateItemDisplay();
        }
    }

}
