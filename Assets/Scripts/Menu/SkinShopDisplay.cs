using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShopDisplay : PageShopDisplay
{
    [SerializeField] private Skin[] _skinItems;

    private void OnEnable()
    {
        SetSelectedMap(0);
    }

    protected override void Init()
    {
        _items = _skinItems;
    }

    protected override void SelectItem()
    {
        AvailabilitySkins.Selectedskin = _skinItems[_selectedItemIndex];
        UpdateItemDisplay();
    }
    protected override bool ItemIsSelect() => _skinItems[_selectedItemIndex] == AvailabilitySkins.Selectedskin;

    protected override bool CheckPurchased()
    {
        for (int i = 0; i < AvailabilitySkins.SkinCount; i++) if (AvailabilitySkins.GetSkin(i) == _skinItems[_selectedItemIndex]) return true;
        return false;
    }

    public override void BuySelectedMap()
    {
        if (CoinWallet.TryMakePurchase(_skinItems[_selectedItemIndex].ItemPrice))
        {
            AvailabilitySkins.AddSkin(_skinItems[_selectedItemIndex]);
            SelectItem();
            UpdateItemDisplay();
        }
    }

}