using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _money;

    public int Money => _money;

    public void Pay(int price)
    {
        _money -= price;
    }

    public void GetReward(int money)
    {
        _money += money;
    }

    public void LoadData(WalletData data)
    {
        _money = data.Money;
    }
}