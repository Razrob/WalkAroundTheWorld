using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AvailabilitySkins
{
    public static List<Skin> Skins = new List<Skin>();

    public static Skin SelectedSkin { get; set; }
    public static int SkinCount => Skins.Count;

    public static event Action OnSkinsChanged;

    public static void AddSkin(Skin _skin)
    {
        Skins.Add(_skin);
        if (Skins.Count == 1) SetSelectedSkin(Skins[0]);
        OnSkinsChanged?.Invoke();
    }

    public static Skin GetSkin(int _index)
    {
        if (_index < 0 || _index >= Skins.Count) return null;
        return Skins[_index];
    }

    public static void SetSelectedSkin(Skin _skin)
    {
        SelectedSkin = _skin;
        OnSkinsChanged?.Invoke();
    }

}
