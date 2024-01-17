using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LeaderboardUpdater : MonoBehaviour
{
    private int _oldScore;
    private float _timer;

    private bool _authDialogWasShown;

    private void Awake()
    {
        YandexGame.DoAfterInit(() =>
        {
            try
            {
                JSBridge.YandexGame_GameReady();
            }
            catch { }

            _oldScore = CalculateCurrentPlayerScore();
            UpdateScore();
        });
    }

    private void Update()
    {
        if (!YandexGame.SDKEnabled)
            return;

        _timer += Time.deltaTime;

        if (_timer < 7)
            return;

        _timer = 0f;
        UpdateScore();
    }

    private int CalculateCurrentPlayerScore()
    {
        int score = 0;

        score += PlayerStats.Steps;

        //implement earn money

        return score;
    }

    private void UpdateScore()
    {
        int newScore = CalculateCurrentPlayerScore();

        if (newScore <= _oldScore)
            return;

        _oldScore = newScore;
        Debug.Log("new score: " + newScore);


#if !UNITY_EDITOR

        if (YandexGame.auth)
            JSBridge.SetLeaderboardScore(newScore);
#endif
    }
}
