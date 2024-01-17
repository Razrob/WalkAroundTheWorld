using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using YG;

public class AdsShower : MonoBehaviour
{
    [SerializeField] private AdScreen _adScreen;

    private float FULLSCREEN_INTERVAL = 75f;
    private float _lastFullscreenShowTime;
    private bool _interCoroutineIsActive;

    private void Awake()
    {
        YandexGame.DoAfterInit(() =>
        {
            //_adScreen = UIScreenRepository.GetScreen<AdScreen>();
            YandexGame.RewardVideoEvent += OnRewardShow;
        });
    }
    
    private void Update()
    {
        if (!YandexGame.SDKEnabled)
            return;

        if (CheckTimer())
            StartCoroutine(ShowInter());
    }

    private void OnRewardShow(int id)
    {
        _lastFullscreenShowTime = Time.time;
    }

    private bool CheckTimer() => Time.time - _lastFullscreenShowTime >= FULLSCREEN_INTERVAL;

    private IEnumerator ShowInter()
    {
        if (_interCoroutineIsActive)
            yield break;

        _lastFullscreenShowTime = Time.time;
        _interCoroutineIsActive = true;

        bool prepared = false;
        Action action = () => prepared = true;
        _adScreen.OpenInterPreparing(action);

        while (!prepared)
            yield return null;

        YandexGame.FullscreenShow();

        yield return new WaitForSeconds(0.5f);

        while (YandexGame.nowAdsShow)
            yield return null;

        _adScreen.HidePrepareWindow();
        _lastFullscreenShowTime = Time.time;
        _interCoroutineIsActive = false;
    }
}
