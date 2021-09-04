using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWindowsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] _windows;


    private int _activeWindowIndex;

    private void OnEnable()
    {
        SetActiveWindow(0);
    }

    private void UpdateWindow()
    {
        foreach (GameObject _window in _windows) _window.SetActive(false);
        _windows[_activeWindowIndex].SetActive(true);
    }

    public void SetActiveWindow(int _windowIndex)
    {
        _activeWindowIndex = _windowIndex;
        UpdateWindow();
    }

    public void ChangeWindow(int _indexOffcet)
    {
        if (_activeWindowIndex + _indexOffcet < 0 || _activeWindowIndex + _indexOffcet >= _windows.Length) return;

        _activeWindowIndex += _indexOffcet;
        UpdateWindow();
    }

}
