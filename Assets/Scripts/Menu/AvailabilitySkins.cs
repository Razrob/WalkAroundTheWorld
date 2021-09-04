using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailabilitySkins
{
    private static List<Skin> _skins = new List<Skin>();

    public static Skin Selectedskin { get; set; }
    public static int SkinCount => _skins.Count;

    public static void AddSkin(Skin _skin)
    {
        _skins.Add(_skin);
        if (_skins.Count == 1) Selectedskin = _skins[0];
    }

    public static Skin GetSkin(int _index)
    {
        if (_index < 0 || _index >= _skins.Count) return null;
        return _skins[_index];
    }

}
