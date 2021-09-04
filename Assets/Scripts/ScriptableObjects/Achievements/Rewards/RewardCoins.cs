using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Create achievement rewards/Coin reward", order = 5)]
public class RewardCoins : AchievementReward
{
    [SerializeField] private int _coinCount;
    [SerializeField] private Sprite _sprite;

    public override int RewardCount => _coinCount;
    public override Sprite RewardSprite => _sprite;


    public override void TakeReward() => CoinWallet.Balance += _coinCount;
    
}
