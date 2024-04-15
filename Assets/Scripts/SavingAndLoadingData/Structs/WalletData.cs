using System;

[Serializable]
public struct WalletData
{
    public int Money;

    public WalletData(Wallet wallet)
    {
        Money = wallet.Money;
    }
}