using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable, ISpawnable
{
    [SerializeField] private int _denomination;
    [SerializeField] private float _spawnChance;
    [SerializeField] private float _spawnHeightOffcet;

    public float SpawnChance => _spawnChance;
    public float SpawnHeightOffcet => _spawnHeightOffcet;

    public void Collect()
    {
        CoinWallet.SetBalance(CoinWallet.Balance + _denomination);
        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
