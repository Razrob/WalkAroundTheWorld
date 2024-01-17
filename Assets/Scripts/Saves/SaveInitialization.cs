using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveInitialization : MonoBehaviour
{
    private void Awake()
    {
        YandexGame.DoAfterInit(() =>
        {
            GameSaver.Init();
            GameSaver.LoadGameSaves();
        });
    }
}
