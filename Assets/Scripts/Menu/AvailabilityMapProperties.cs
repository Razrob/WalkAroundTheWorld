using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailabilityMapProperties
{

    private static List<MapProperties> _mapProperties = new List<MapProperties>();

    public static MapProperties SelectedMap { get; set; }
    public static int MapCount => _mapProperties.Count;
    

    public static void AddMapProperties(MapProperties _map)
    {
        _mapProperties.Add(_map);
        if (_mapProperties.Count == 1) SelectedMap = _mapProperties[0];
    }

    public static MapProperties GetMapProperties(int _index)
    {
        if (_index < 0 || _index >= _mapProperties.Count) return null;
        return _mapProperties[_index];
    }

}
