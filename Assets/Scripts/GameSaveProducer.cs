using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class GameSaveProducer : MonoBehaviour
{
    private bool _blocked;
    private bool _inited = true;

    private void Awake()
    {
        YandexGame.DoAfterInit(() =>
        {
            Sequence saveTween = DOTween.Sequence();
            saveTween.AppendInterval(5f);
            saveTween.AppendCallback(() => _inited = true);
            saveTween.AppendCallback(Save);
            saveTween.SetLoops(-1);
        });
    }

    private void Save()
    {
        if (!_blocked && _inited)
        {
            GameSaver.ForceSave();
            YandexGame.SaveProgress();
            _blocked = true;
            DOTween.Sequence().AppendInterval(1.5f).AppendCallback(() => _blocked = false);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (YandexGame.SDKEnabled && !focus && _inited)
        {
            GameSaver.ForceSave();
            YandexGame.SaveProgress();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (YandexGame.SDKEnabled && !pause && _inited)
        {
            GameSaver.ForceSave();
            YandexGame.SaveProgress();
        }
    }

    private void OnApplicationQuit()
    {
        if (YandexGame.SDKEnabled && _inited)
        {
            GameSaver.ForceSave();
            YandexGame.SaveProgress();
        }
    }
}
