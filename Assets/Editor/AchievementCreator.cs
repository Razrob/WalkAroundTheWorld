using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AchievementCreator : EditorWindow
{
    private string _achievementFileName = "Achievement_";
    private string _achievementPath = "Resources/Achievements";
    private string _conditionPath = "Resources/Achievements/Conditions";
    private string _rewardPath = "Resources/Achievements/Rewards";

    private ConditionType _conditionType;
    private RewardType _rewardType;


    private string _achievementInfo;

    [MenuItem("Window/AchievementCreator")]
    private static void CreateWindow()
    {
        AchievementCreator _window = GetWindow<AchievementCreator>();
        _window.Show();
    }

    private void OnGUI()
    {

        GUILayout.Space(10);

        GUILayout.Label("Achievement file name");
        _achievementFileName = GUILayout.TextField(_achievementFileName);

        GUILayout.Label("Achievement path");
        _achievementPath = GUILayout.TextField(_achievementPath);

        GUILayout.Label("Condition path");
        _conditionPath = GUILayout.TextField(_conditionPath);

        GUILayout.Label("Reward path");
        _rewardPath = GUILayout.TextField(_rewardPath);


        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("RewardType");
        _rewardType = (RewardType)EditorGUILayout.EnumPopup(_rewardType);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.Label("ConditionType");
        _conditionType = (ConditionType)EditorGUILayout.EnumPopup(_conditionType);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        GUILayout.Label("Achievement info");
        _achievementInfo = GUILayout.TextField(_achievementInfo);
        GUILayout.Space(5);

        if (GUILayout.Button("Create achievement")) CreateAchievement();
    }

    private void CreateAchievement()
    {
        Achievement _achievement = CreateInstance<Achievement>();
        _achievement._achievementInfo = _achievementInfo;

        AchievementCondition _achievementCondition = null;
        AchievementReward _achievementReward = null;


        if (_conditionType == ConditionType.StepsAllTime) _achievementCondition = CreateInstance<ConditionAllTimeSteps>();
        if (_rewardType == RewardType.Coins) _achievementReward = CreateInstance<RewardCoins>();


        _achievement._achievementCondition = _achievementCondition;
        _achievement._achievementReward = _achievementReward;

        AssetDatabase.CreateAsset(_achievementCondition, $"Assets/{_conditionPath}/Condition{_achievementFileName.Substring(_achievementFileName.Length - 4)}.asset");
        AssetDatabase.CreateAsset(_achievementReward, $"Assets/{_rewardPath}/Reward{_achievementFileName.Substring(_achievementFileName.Length - 4)}.asset");
        AssetDatabase.CreateAsset(_achievement, $"Assets/{_achievementPath}/{_achievementFileName}.asset");


    }

}
public enum ConditionType
{
    StepsAllTime
}
public enum RewardType
{
    Coins
}