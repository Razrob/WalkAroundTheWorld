using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapPropertiesChoice : MonoBehaviour
{
    [SerializeField] private Image _mapImage;
    [SerializeField] private TextMeshProUGUI _mapCount;

    private int _activeMapIndex;

    private void OnEnable()
    {
        SetActiveMap(0);
    }

    private void UpdateSelectedMap()
    {
        if (AvailabilityMapProperties.GetMapProperties(_activeMapIndex) == null) return;

        _mapImage.sprite = AvailabilityMapProperties.GetMapProperties(_activeMapIndex).ItemImage;
        _mapCount.text = $"{_activeMapIndex + 1} / {AvailabilityMapProperties.MapCount}";
      //  AvailabilityMapProperties.SelectedMap = _ma[_activeMapIndex];
    }

    public void ChangeMap(int _indexOffcet)
    {
        if (_activeMapIndex + _indexOffcet < 0 || _activeMapIndex + _indexOffcet >= AvailabilityMapProperties.MapCount) return;

        _activeMapIndex += _indexOffcet;
        UpdateSelectedMap();
    }

    public void SetActiveMap(int _index)
    {
        _activeMapIndex = 0;
        UpdateSelectedMap();
    }

}
