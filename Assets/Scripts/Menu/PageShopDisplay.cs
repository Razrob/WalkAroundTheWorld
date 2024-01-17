using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using YG;

public abstract class PageShopDisplay : MonoBehaviour
{
    [SerializeField] protected ShopPageUI _mapUI;

    protected IShopItem[] _items;

    protected bool _block;
    protected int _selectedItemIndex;

    protected abstract bool ItemIsSelect();
    protected abstract void SelectItem();
    protected abstract bool CheckPurchased();
    public abstract void BuySelectedItem(bool isFree);
    public abstract bool PurchaseAvailable();
    public abstract RewardAdType RewardAdType();


    protected void UpdateItemDisplay()
    {
        if (_items == null || _items.Length <= _selectedItemIndex) return;
        

        bool _isPurchased = CheckPurchased();
        _mapUI.ItemImage.sprite = _items[_selectedItemIndex].ItemImage;
        _mapUI.ItemPrice.text = _items[_selectedItemIndex].ItemPrice.ToString();
        _mapUI.ItemName.text = _items[_selectedItemIndex].ItemName;
        _mapUI.BuyButton.interactable = true;

        _mapUI.BuyButton.onClick.RemoveListener(SelectItem);
        _mapUI.BuyButton.onClick.RemoveListener(OnBuyClick);
        _mapUI.BuyButtonRewardIcon.SetActive(false);

        if (_isPurchased)
        {
            if (ItemIsSelect())
            {
                _mapUI.BuyButtonText.text = "Выбрано";
                _mapUI.BuyButton.interactable = false;
            }
            else
            {
                _mapUI.BuyButtonText.text = "Выбрать";
                _mapUI.BuyButton.onClick.AddListener(SelectItem);
            }
        }
        else
        {
            _mapUI.BuyButtonText.text = "Купить";
            _mapUI.BuyButton.onClick.AddListener(OnBuyClick);
            _mapUI.BuyButtonRewardIcon.SetActive(!PurchaseAvailable());
        }

        _mapUI.DarkPanel.SetActive(!_isPurchased);
    }

    private void OnBuyClick()
    {
        if (PurchaseAvailable())
            BuySelectedItem(false);
        else
        {
            YandexGame.RewardVideoEvent += OnAdReward;
            YandexGame.CloseVideoEvent += OnAdClose;
            YandexGame.ErrorVideoEvent += OnAdError;
            _block = true;
            GetComponentInParent<GraphicRaycaster>().enabled = false;

            YandexGame.RewVideoShow((int)RewardAdType());
        }
    }

    private void OnAdError()
    {
        _block = false;
        GetComponentInParent<GraphicRaycaster>().enabled = true;

        YandexGame.RewardVideoEvent -= OnAdReward;
        YandexGame.CloseVideoEvent -= OnAdClose;
        YandexGame.ErrorVideoEvent -= OnAdError;
    }

    private void OnAdClose()
    {
        _block = false;
        GetComponentInParent<GraphicRaycaster>().enabled = true;
        YandexGame.RewardVideoEvent -= OnAdReward;
        YandexGame.CloseVideoEvent -= OnAdClose;
        YandexGame.ErrorVideoEvent -= OnAdError;
    }

    private void OnAdReward(int obj)
    {
        _block = false;
        GetComponentInParent<GraphicRaycaster>().enabled = true;
        BuySelectedItem(true);
        YandexGame.RewardVideoEvent -= OnAdReward;
        YandexGame.CloseVideoEvent -= OnAdClose;
        YandexGame.ErrorVideoEvent -= OnAdError;
    }

    public void SetSelectedItem(int _index)
    {
        _selectedItemIndex = 0;
        UpdateItemDisplay();
    }

    public void ChangeItem(int _indexOffcet)
    {
        if (_block)
            return;

        if (_selectedItemIndex + _indexOffcet < 0 || _selectedItemIndex + _indexOffcet >= _items.Length) return;

        _selectedItemIndex += _indexOffcet;
        UpdateItemDisplay();

    }




    [Serializable]
    protected class ShopPageUI
    {
        public Image ItemImage;
        public TextMeshProUGUI ItemPrice;
        public TextMeshProUGUI ItemName;
        public TextMeshProUGUI BuyButtonText;
        public Button BuyButton;
        public GameObject BuyButtonRewardIcon;
        public GameObject DarkPanel;
    }
}
