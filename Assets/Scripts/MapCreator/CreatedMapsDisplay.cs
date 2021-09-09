using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatedMapsDisplay : MonoBehaviour
{
    [SerializeField] private MapCreatorWindowsDisplay _mapCreatorWindowsDisplay;

    [SerializeField] private Button _editButton;
    [SerializeField] private Button _deleteButton;

    [SerializeField] private GameObject _mapImage;
    [SerializeField] private TextMeshProUGUI _mapName;

    private int _activeMapIndex;

    private void OnEnable()
    {
        SetActiveMapDisplay(0);
    }

    private void UpdateMapDisplay()
    {
        bool _mapAvailable = AvailabilityMapProperties.CustomMapCount > 0;
        _mapImage.SetActive(_mapAvailable);

        if (!_mapAvailable) _mapName.text = "Созданные карты отсутствуют";
        else _mapName.text = AvailabilityMapProperties.GetCustomMapProperties(_activeMapIndex).ItemName;

        _editButton.interactable = _mapAvailable;
        _deleteButton.interactable = _mapAvailable;
    }

    public void ChangeActiveMapDisplay(int _indexOffcet)
    {
        if (_activeMapIndex + _indexOffcet < 0 || _activeMapIndex + _indexOffcet >= AvailabilityMapProperties.CustomMapCount) return;
        _activeMapIndex += _indexOffcet;
        UpdateMapDisplay();
    }

    public void SetActiveMapDisplay(int _index)
    {
        if (_index < 0 || _index >= AvailabilityMapProperties.CustomMapCount)
        {
            if (AvailabilityMapProperties.CustomMapCount == 0) _index = 0;
            else return;
        }
        _activeMapIndex = _index;
        UpdateMapDisplay();
    }

    public void DeleteActiveMap()
    {
        AvailabilityMapProperties.RemoveCustomMapProperties(_activeMapIndex);
        SetActiveMapDisplay(0);
    }

    public void EditActiveMap()
    {
        if(_activeMapIndex < AvailabilityMapProperties.CustomMapCount) _mapCreatorWindowsDisplay.OpenMapCreator(_activeMapIndex);
    }
    public void CreateNewMap() => _mapCreatorWindowsDisplay.OpenMapCreator();
}
