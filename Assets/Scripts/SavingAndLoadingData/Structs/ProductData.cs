using System;

[Serializable]
public struct ProductData
{
    public bool IsBuyed;

    public ProductData(Product product)
    {
        IsBuyed = product.IsBuyed;
    }
}