using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapCreatorWindowsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] _creatorWindows;
    [SerializeField] private MapCreator _mapCreator;

    [SerializeField] private GameObject _darkPanel;
    [SerializeField] private AchievementCondition _creatorAvailabilityCondition;
    [SerializeField] private Slider _conditionProgress;

    private void OnEnable()
    {
        _darkPanel.SetActive(!_creatorAvailabilityCondition.CheckComplete());

        if (_creatorAvailabilityCondition.CheckComplete()) SetActiveDisplay(0);
        else _conditionProgress.value = _creatorAvailabilityCondition.GetProgress();
    }

    public void SetActiveDisplay(int _index)
    {
        if (_index < 0 || _index >= _creatorWindows.Length) return;

        foreach (GameObject _window in _creatorWindows) _window.SetActive(false);
        _creatorWindows[_index].SetActive(true);
    }

    public void OpenMapCreator(int _editableMapIndex = -1)
    {
        foreach (GameObject _window in _creatorWindows) _window.SetActive(false);
        _mapCreator.gameObject.SetActive(true);
        if (_editableMapIndex > -1) _mapCreator.SetEditableMapProperties(_editableMapIndex);
    }
}
