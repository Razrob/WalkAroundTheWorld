using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

[SelectionBase]
public class AudioSourceHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioType _audioType;

    private Tween _audioTween;

    public AudioType AudioType => _audioType;
    public AudioSource AudioSource => _audioSource;
    public float StartVolume { get; private set; }
    public bool IsActive { get; private set; }

    private void Awake()
    {
        if (!AudioSource)
            return;

        StartVolume = _audioSource.volume;
        IsActive = !_audioSource.mute;
    }

    public void SetAudioSourceActive(bool value, float duration = 0f)
    {
        if (!AudioSource)
            return;

        _audioTween?.Kill(true);

        if (value)
        {
            AudioSource.time = 0f;
            AudioSource.volume = 0f;
            AudioSource.Play();

            if (duration > 0.001f)
                _audioTween = DOTween.To(() => AudioSource.volume, value => AudioSource.volume = value, StartVolume, duration);
            else
                AudioSource.volume = StartVolume;
        }
        else
            _audioTween = DOTween.To(() => AudioSource.volume, value => AudioSource.volume = value, 0, duration)
                .OnComplete(() => AudioSource.Stop());

        IsActive = value;
    }

    public void SetVolume(float clampedValue, float duration = 0f)
    {
        if (!AudioSource)
            return;

        _audioTween?.Kill();

        _audioTween = DOTween.To(() => AudioSource.volume, 
            value => AudioSource.volume = value, Mathf.Clamp01(clampedValue) * StartVolume, duration);
    }
}