using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AchievementsDisplay : MonoBehaviour
{
    [SerializeField] private RectTransform _achievementsParent;
    [SerializeField] private int _achievementHeight;

    [SerializeField] private AchievementUI _achievementPrefab;

    [SerializeField] private Achievement[] _achievements;

    private List<AchievementUI> _achievementUIs = new List<AchievementUI>();

    public static event Action OnRewardTaken;

    private void Start()
    {
        _achievementsParent.sizeDelta = new Vector2(_achievementsParent.sizeDelta.x, _achievementHeight * _achievements.Length);

        for (int i = 0; i < _achievements.Length; i++)
        {
            _achievementUIs.Add(Instantiate(_achievementPrefab, _achievementsParent));

            UpdateAchievementUI(_achievementUIs[i], i);
        }
    }

    private void UpdateAchievementUI(AchievementUI _achievementUI, int _achievementIndex)
    {
        _achievementUI.AchievementInfo.text = _achievements[_achievementIndex].AchievementInfo;
        _achievementUI.ValueCount.text = $"{_achievements[_achievementIndex].CurrentValue} / {_achievements[_achievementIndex].TargetValue}";
        _achievementUI.ProgressSlider.value = _achievements[_achievementIndex].GetProgress();
        _achievementUI.RewardCount.text = _achievements[_achievementIndex].RewardCount.ToString();
        _achievementUI.RewardImage.sprite = _achievements[_achievementIndex].RewardSprite;

        _achievementUI.TakeButton.interactable = _achievements[_achievementIndex].CheckComplete() && !_achievements[_achievementIndex].RewardIsReceived;

        bool _isReceived = _achievements[_achievementIndex].RewardIsReceived;
        _achievementUI.DarkPanel.SetActive(_isReceived);
        if (_isReceived) _achievementUI.TakeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Получено";
        else
        {
            _achievementUI.TakeButton.onClick.AddListener(() => TakeReward(_achievementIndex));
            _achievementUI.TakeButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Забрать";
        }
        
    }

    public void TakeReward(int _achievementIndex)
    {
        _achievements[_achievementIndex].TakeReward();
        UpdateAchievementUI(_achievementUIs[_achievementIndex], _achievementIndex);
        OnRewardTaken?.Invoke();
    }
}
