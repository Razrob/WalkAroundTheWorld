using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SavableCompletedAchievements
{
    public SavableAchievement[] SavableAchievements;


    [Serializable]
    public class SavableAchievement
    {
        public bool IsComplete;
        public string AchievementName;
    }
}
