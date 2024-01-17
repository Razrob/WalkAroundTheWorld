using System;
using UnityEngine;

[Serializable]
public class SettingsData
{
    [SerializeField] private bool _audioEnabled;

    public bool AudioEnabled => _audioEnabled;

    public event Action<bool> OnAudioChange;

    public SettingsData()
    {
        _audioEnabled = true;
    }

    public void SetAudionActive(bool active)
    {
        _audioEnabled = active;
        OnAudioChange?.Invoke(active);
    }
}