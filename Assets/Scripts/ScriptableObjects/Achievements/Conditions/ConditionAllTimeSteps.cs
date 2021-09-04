using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Create achievement conditions/All time steps condition", order = 5)]
public class ConditionAllTimeSteps : AchievementCondition
{
    [SerializeField] private int _targetValue;

    public override int TargetValue => _targetValue;
    public override int CurrentValue => PlayerStats.Steps;


    public override bool CheckComplete() => _targetValue <= CurrentValue;
    public override float GetProgress()
    {
        float _value = CurrentValue * 1f / _targetValue;
        if (_value > 1) _value = 1;
        return _value;
    }

}
