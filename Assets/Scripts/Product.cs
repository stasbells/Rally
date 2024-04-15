using System;
using UnityEngine;

[Serializable]
public abstract class Product : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private bool _isDefoult;

    private bool _isBuyed = false;

    public int Price => _price;
    public bool IsBuyed => _isBuyed;

    private void Awake()
    {
        if (_isDefoult)
            _isBuyed = true;
    }

    public void SetBuyed() { _isBuyed = true; }

    public void LoadData(ProductData data)
    {
        _isBuyed = data.IsBuyed;
    }
}