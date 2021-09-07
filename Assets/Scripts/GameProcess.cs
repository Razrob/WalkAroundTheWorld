using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProcess : MonoBehaviour
{
    [SerializeField] private UIDisplay _UIDisplay;

    private int _startCoins;
    private int _startSteps;

    private void Awake()
    {
        _startCoins = CoinWallet.Balance;
        _startSteps = PlayerStats.Steps;

        Time.timeScale = 1;
        PlayerStats.OnHealthIsZero += FinishGame;
        Instantiate(AvailabilitySkins.SelectedSkin.PlayerSkin, Vector3.zero, AvailabilitySkins.SelectedSkin.PlayerSkin.transform.rotation);
    }


    private void FinishGame()
    {
        Time.timeScale = 0;
        _UIDisplay.SetGameoverPanelActive(true);
        _UIDisplay.DisplayRewards(CoinWallet.Balance - _startCoins, PlayerStats.Steps - _startSteps);
    }

    public void Pause()
    {
        Time.timeScale = Mathf.Abs(Time.timeScale - 1);
        _UIDisplay.SetPausePanelActive(!System.Convert.ToBoolean(Time.timeScale));
    }
    public void ExitToMenu() => SceneManager.LoadScene("Menu");
    public void RestartGame() => SceneManager.LoadScene("Game");

}
