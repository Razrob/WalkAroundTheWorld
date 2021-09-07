using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Image _playerPhoto;

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameoverPanel;
    [SerializeField] private TextMeshProUGUI _coinRewardText;
    [SerializeField] private TextMeshProUGUI _makeStepsText;

    private void Awake()
    {
        CoinWallet.OnBalanceChanged += UpdateBalanceDisplay;
        PlayerStats.OnHealthChanged += UpdateHealthDisplay;

        PlayerStats.MaxHealth = AvailabilitySkins.SelectedSkin.MaxHealth;
        PlayerStats.Health = AvailabilitySkins.SelectedSkin.MaxHealth;

        UpdateBalanceDisplay(CoinWallet.Balance);
        UpdateHealthDisplay(PlayerStats.GetFloatHealth());


        _playerPhoto.sprite = AvailabilitySkins.SelectedSkin.ItemImage;


    }

    private void UpdateBalanceDisplay(int _balance)
    {
        _balanceText.text = _balance.ToString();
    }
    private void UpdateHealthDisplay(float _health)
    {
        _healthSlider.value = _health;
    }

    private void OnDestroy()
    {
        PlayerStats.NullifyAllEvents();
        CoinWallet.OnBalanceChanged -= UpdateBalanceDisplay;
    }

    public void SetPausePanelActive(bool _enabled) => _pausePanel.SetActive(_enabled);
    public void SetGameoverPanelActive(bool _enabled) => _gameoverPanel.SetActive(_enabled);

    public void DisplayRewards(int _coins, int _steps)
    {
        _coinRewardText.text = _coins.ToString();
        _makeStepsText.text = _steps.ToString();
    }

}
