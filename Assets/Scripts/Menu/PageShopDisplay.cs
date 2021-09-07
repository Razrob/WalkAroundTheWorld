using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public abstract class PageShopDisplay : MonoBehaviour
{
    [SerializeField] protected ShopPageUI _mapUI;

    protected IShopItem[] _items;

    protected int _selectedItemIndex;

    protected abstract bool ItemIsSelect();
    protected abstract void SelectItem();
    protected abstract bool CheckPurchased();
    public abstract void BuySelectedItem();

    

    protected void UpdateItemDisplay()
    {
        if (_items == null || _items.Length <= _selectedItemIndex) return;
        

        bool _isPurchased = CheckPurchased();
        _mapUI.ItemImage.sprite = _items[_selectedItemIndex].ItemImage;
        _mapUI.ItemPrice.text = _items[_selectedItemIndex].ItemPrice.ToString();
        _mapUI.ItemName.text = _items[_selectedItemIndex].ItemName;
        _mapUI.BuyButton.interactable = true;
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
                Button.ButtonClickedEvent _clickedEvent = new Button.ButtonClickedEvent();
                _clickedEvent.AddListener(SelectItem);
                _mapUI.BuyButton.onClick = _clickedEvent;
            }
        }
        else
        {
            _mapUI.BuyButtonText.text = "Купить";
            Button.ButtonClickedEvent _clickedEvent = new Button.ButtonClickedEvent();
            _clickedEvent.AddListener(BuySelectedItem);
            _mapUI.BuyButton.onClick = _clickedEvent;
        }

        _mapUI.DarkPanel.SetActive(!_isPurchased);
    }

    public void SetSelectedItem(int _index)
    {
        _selectedItemIndex = 0;
        UpdateItemDisplay();
    }

    public void ChangeItem(int _indexOffcet)
    {
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
        public GameObject DarkPanel;
    }
}
