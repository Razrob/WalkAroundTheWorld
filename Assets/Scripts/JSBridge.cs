using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class JSBridge
{
    [DllImport("__Internal")]
    public static extern void YandexGame_GameReady();

    [DllImport("__Internal")]
    public static extern void SetLeaderboardScore(int score);
}
