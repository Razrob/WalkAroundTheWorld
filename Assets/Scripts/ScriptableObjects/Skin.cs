using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Create skin", order = 2)]
public class Skin : ScriptableObject, IShopItem
{
    [Header("For game")]
    [SerializeField] private GameObject _playerSkin;
    [SerializeField] private int _maxHealth;

    [Header("For shop")]
    [SerializeField] private Sprite _skinImage;
    [SerializeField] private int _skinPrice;
    [SerializeField] public string _skinName;

    public GameObject PlayerSkin => _playerSkin;
    public int MaxHealth => _maxHealth;


    public Sprite ItemImage => _skinImage;
    public int ItemPrice => _skinPrice;
    public string ItemName => _skinName;
}
