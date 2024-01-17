using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YG;

public class AudioProducer : MonoBehaviour
{
    private bool _isPaused;
    private bool _isFocus;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        YandexGame.DoAfterInit(() =>
        {
            AudioStorage.Get(AudioType.Background)?.SetAudioSourceActive(true, 5f);
            AudioListener.pause = false;

            //foreach (Button button in FindObjectsOfType<Button>(true))
            //    button.onClick.AddListener(() => AudioStorage.Get(AudioType.Click)?.SetAudioSourceActive(true));

            OnAudioChange(YandexGame.savesData.AudioEnable);
        });
    }

    private void OnApplicationPause(bool pause)
    {
        _isPaused = pause;
        RefreshAudio();
    }

    private void OnApplicationFocus(bool focus)
    {
        _isFocus = focus;
        RefreshAudio();
    }

    private void OnAudioChange(bool value)
    {
        AudioListener.pause = !value;
        RefreshAudio();
    }

    private void Update()
    {
        if (!YandexGame.SDKEnabled)
            return;

        RefreshAudio();

        if (Camera.main)
            AudioStorage.Transform.position = Camera.main.transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> list = new List<RaycastResult>(1);
            EventSystem.current.RaycastAll(pointer, list);

            if (list.Count(r => r.gameObject.transform.TryGetComponent(out Button component)) > 0)
            {
                AudioStorage.Get(AudioType.Click)?.SetAudioSourceActive(true);
            }
        }
    }

    private void RefreshAudio()
    {
        bool audioEnable = YandexGame.savesData.AudioEnable
            && !YandexGame.nowVideoAd && !YandexGame.nowFullAd && !YandexGame.nowAdsShow && !_isPaused && _isFocus;

        if (AudioListener.pause == audioEnable)
            AudioListener.pause = !audioEnable;
    }
}
