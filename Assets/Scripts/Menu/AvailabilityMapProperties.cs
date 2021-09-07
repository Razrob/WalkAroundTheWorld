using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AvailabilityMapProperties
{

    public static List<MapProperties> MapProperties = new List<MapProperties>();
    public static List<CustomMapProperties> CustomMapProperties = new List<CustomMapProperties>();

    public static MapProperties SelectedMap { get; set; }
    public static int MapCount => MapProperties.Count;
    public static int CustomMapCount => CustomMapProperties.Count;

    public static event Action OnMapPropertiesChanged;
    public static event Action OnCustomMapPropertiesChanged;


    public static void AddMapProperties(MapProperties _map)
    {
        MapProperties.Add(_map);
        if (MapProperties.Count == 1) SetSelectedMap(MapProperties[0]);
        OnMapPropertiesChanged?.Invoke();
    }

    public static MapProperties GetMapProperties(int _index)
    {
        if (_index < 0 || _index >= MapProperties.Count) return null;
        return MapProperties[_index];
    }


    public static void AddCustomMapProperties(CustomMapProperties _map)
    {
        CustomMapProperties.Add(_map);
        OnCustomMapPropertiesChanged?.Invoke();
    }

    public static CustomMapProperties GetCustomMapProperties(int _index)
    {
        if (_index < 0 || _index >= CustomMapProperties.Count) return null;
        return CustomMapProperties[_index];
    }

    public static void RemoveCustomMapProperties(int _index)
    {
        if (_index < 0 || _index >= CustomMapProperties.Count) return;
        if (CustomMapProperties[_index] == SelectedMap) SelectedMap = MapProperties[0];
        CustomMapProperties.RemoveAt(_index);
    }

    public static void SetSelectedMap(MapProperties _mapProperties)
    {
        SelectedMap = _mapProperties;
        OnMapPropertiesChanged?.Invoke();
    }

}
