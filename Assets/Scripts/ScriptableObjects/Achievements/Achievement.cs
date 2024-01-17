using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Create achievement", order = 3)]
public class Achievement : ScriptableObject
{
    [SerializeField] public string _achievementInfo;

    [SerializeField] public AchievementCondition _achievementCondition;
    [SerializeField] public AchievementReward _achievementReward;

    //[field: NonSerialized] public bool RewardIsReceived { get; set; }

    public string AchievementInfo => _achievementInfo;
    public int TargetValue => _achievementCondition.TargetValue;
    public int CurrentValue => _achievementCondition.CurrentValue;

    public int RewardCount => _achievementReward.RewardCount;
    public Sprite RewardSprite => _achievementReward.RewardSprite;

    public static HashSet<string> ReceivedAchievements = new HashSet<string>();

    public bool CheckComplete() => _achievementCondition.CheckComplete();
    public float GetProgress() => _achievementCondition.GetProgress();

    public void TakeReward()
    {
        ReceivedAchievements.Add(name);
        _achievementReward.TakeReward();
    }
    
}
