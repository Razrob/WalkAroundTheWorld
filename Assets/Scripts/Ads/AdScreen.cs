using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class AdScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _interCanvasGroup;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Transform _rotateObject;
    [SerializeField] private float _rotateSpeed;

    public void OpenInterPreparing(Action onPrepare)
    {
        _rotateObject.rotation = Quaternion.identity;
        _interCanvasGroup.DOKill();
        _interCanvasGroup.DOFade(1f, 0.4f);
        _interCanvasGroup.blocksRaycasts = true;

        SetInterPrepareText(4);

        DOTween.To(() => 4, value => SetInterPrepareText(value), 0, 3f).SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                _rotateObject.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * _rotateSpeed);
            })
            .OnComplete(() =>
            {
                onPrepare();
            });
    }

    public void HidePrepareWindow()
    {
        _interCanvasGroup.DOKill();
        _interCanvasGroup.alpha = 0f;
        _interCanvasGroup.blocksRaycasts = false;
    }

    private void SetInterPrepareText(int secs)
    {
        _text.text = $"Реклама через: {secs} сек";
    }
}