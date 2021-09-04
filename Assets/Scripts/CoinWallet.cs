using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinWallet
{
    private static int _balance = 10000;
    public static int Balance
    {
        get { return _balance; }
        set 
        { 
            if (value > 0) _balance = value;
            OnBalanceChanged?.Invoke(_balance);
        }
    }

    public static event Action<int> OnBalanceChanged;


    public static bool TryMakePurchase(int _value)
    {
        if(_balance >= _value)
        {
            _balance -= _value;
            OnBalanceChanged?.Invoke(_balance);
            return true;
        }
        return false;
    }


}
