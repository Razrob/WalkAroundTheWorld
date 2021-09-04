using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementReward : ScriptableObject
{
    public virtual int RewardCount => throw new System.NotImplementedException();
    public virtual Sprite RewardSprite => throw new System.NotImplementedException();

    public virtual void TakeReward() => throw new System.NotImplementedException();
}
