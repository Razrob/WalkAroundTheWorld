using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuWindowsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] _windows;
    [SerializeField] private TextMeshProUGUI _balance;

    private void Start()
    {
        Time.timeScale = 1;
        _balance.text = CoinWallet.Balance.ToString();

        ChangeActiveWindow(0);
        CoinWallet.OnBalanceChanged += (_value) => _balance.text = _value.ToString();
    }

    public void ChangeActiveWindow(int _windowIndex)
    {
        if (_windowIndex < 0 || _windowIndex >= _windows.Length) return;
        foreach (GameObject _window in _windows) _window.SetActive(false);
        _windows[_windowIndex].SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
