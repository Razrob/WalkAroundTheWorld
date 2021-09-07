using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinWallet
{
    public static int Balance { get; set; } = 15000;

    public static event Action<int> OnBalanceChanged;

    public static void SetBalance(int _value)
    {
        Balance = _value;
        OnBalanceChanged?.Invoke(Balance);
    }

    public static bool TryMakePurchase(int _value)
    {
        if(Balance >= _value)
        {
            Balance -= _value;
            OnBalanceChanged?.Invoke(Balance);
            return true;
        }
        return false;
    }


}
