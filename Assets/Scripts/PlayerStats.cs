using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats
{
    private static int _health;
    public static int Health 
    {
        get { return _health; }
        set
        {
            if (value < 0) _health = 0;
            else if (value > MaxHealth) _health = MaxHealth;
            else _health = value;
            OnHealthChanged?.Invoke(GetFloatHealth());
            if (_health == 0) OnHealthIsZero?.Invoke();
        }
    }
    public static int MaxHealth { get; set; }



    public static int Steps { get; set; }


    public static event Action<float> OnHealthChanged;
    public static event Action OnHealthIsZero;

    public static event Action<float> OnStepsChanged;

    public static float GetFloatHealth() => _health * 1f / MaxHealth;


    public static void MakeStep()
    {
        Steps++;
        AudioStorage.Get(AudioType.Jump)?.SetAudioSourceActive(true, 0);
        OnStepsChanged?.Invoke(Steps);
    }



    public static void NullifyAllEvents()
    {
        OnHealthIsZero = null;
        OnHealthChanged = null;
    }
}
