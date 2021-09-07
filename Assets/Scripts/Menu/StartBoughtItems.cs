using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoughtItems: MonoBehaviour
{
    [SerializeField] private MapProperties[] _startMaps;
    [SerializeField] private Skin[] _startSkins;

    private void Start()
    {
        if(AvailabilityMapProperties.MapCount == 0) foreach (MapProperties _map in _startMaps) AvailabilityMapProperties.AddMapProperties(_map);
        if (AvailabilitySkins.SkinCount == 0) foreach (Skin _skin in _startSkins) AvailabilitySkins.AddSkin(_skin);

        if(AvailabilityMapProperties.SelectedMap == null) AvailabilityMapProperties.SetSelectedMap(_startMaps[0]);
        if(AvailabilitySkins.SelectedSkin == null) AvailabilitySkins.SetSelectedSkin(_startSkins[0]);

    }

}
