using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Create achievement", order = 3)]
public class Achievement : ScriptableObject
{
    [SerializeField] public string _achievementInfo;

    [SerializeField] public AchievementCondition _achievementCondition;
    [SerializeField] public AchievementReward _achievementReward;

    [SerializeField] public bool _rewardIsReceived;

    public string AchievementInfo => _achievementInfo;
    public int TargetValue => _achievementCondition.TargetValue;
    public int CurrentValue => _achievementCondition.CurrentValue;

    public int RewardCount => _achievementReward.RewardCount;
    public Sprite RewardSprite => _achievementReward.RewardSprite;


    public bool RewardIsReceived => _rewardIsReceived;

    public bool CheckComplete() => _achievementCondition.CheckComplete();
    public float GetProgress() => _achievementCondition.GetProgress();

    public void TakeReward()
    {
        _rewardIsReceived = true;
        _achievementReward.TakeReward();
    }
    
}
