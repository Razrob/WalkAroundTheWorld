using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchievementCondition : ScriptableObject
{
    public virtual int TargetValue => throw new NotImplementedException();
    public virtual int CurrentValue => throw new NotImplementedException();

    public virtual bool CheckComplete() => throw new NotImplementedException();
    public virtual float GetProgress() => throw new NotImplementedException();
}
