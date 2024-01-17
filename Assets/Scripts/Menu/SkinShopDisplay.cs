using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShopDisplay : PageShopDisplay
{
    [SerializeField] private Skin[] _skinItems;

    private void OnEnable()
    {
        Init();
        SetSelectedItem(0);
    }

    private void Init()
    {
        _items = _skinItems;
    }

    protected override void SelectItem()
    {
        AvailabilitySkins.SetSelectedSkin(_skinItems[_selectedItemIndex]);
        UpdateItemDisplay();
    }
    protected override bool ItemIsSelect() => _skinItems[_selectedItemIndex] == AvailabilitySkins.SelectedSkin;

    protected override bool CheckPurchased()
    {
        for (int i = 0; i < AvailabilitySkins.SkinCount; i++) if (AvailabilitySkins.GetSkin(i) == _skinItems[_selectedItemIndex]) return true;
        return false;
    }

    public override void BuySelectedItem(bool isFree)
    {
        if (isFree || CoinWallet.TryMakePurchase(_skinItems[_selectedItemIndex].ItemPrice))
        {
            AvailabilitySkins.AddSkin(_skinItems[_selectedItemIndex]);
            SelectItem();
            UpdateItemDisplay();
        }
    }

    public override bool PurchaseAvailable()
    {
        return CoinWallet.Balance >= _skinItems[_selectedItemIndex].ItemPrice;
    }

    public override RewardAdType RewardAdType()
    {
        return global::RewardAdType.Skin;
    }
}