using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoughtItems: MonoBehaviour
{
    [SerializeField] private MapProperties[] _startMaps;
    [SerializeField] private Skin[] _startSkins;

    private void Awake()
    {
        foreach (MapProperties _map in _startMaps) AvailabilityMapProperties.AddMapProperties(_map);
        foreach (Skin _skin in _startSkins) AvailabilitySkins.AddSkin(_skin);

        if(AvailabilityMapProperties.SelectedMap == null) AvailabilityMapProperties.SelectedMap = _startMaps[0];
        if(AvailabilitySkins.Selectedskin == null) AvailabilitySkins.Selectedskin = _startSkins[0];

    }

}
