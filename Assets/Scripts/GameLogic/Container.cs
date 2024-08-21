using System;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private List<Product> _prefabs;

    private Wallet _wallet;
    private List<Product> _items;

    public int CurrentItemIndex { get; private set; } = 0;
    public int ItemsCount => _prefabs.Count;
    public int WalletCount => _wallet.Money;
    public IReadOnlyList<Product> Items => _items;

    private void Awake()
    {
        _wallet = FindFirstObjectByType<Wallet>();

        if (_items == null)
            Initialize();
    }

    public Product GetItem(int index) => _items[index];

    public void SetCurrentIndex(int index) => CurrentItemIndex = index;

    public void BuyItem(Product item)
    {
        item.SetBuyed();
        _wallet.Pay(item.Price);
    }

    public int TryGetIndexOfActiveProduct()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if(_items[i].gameObject.activeSelf)
                return i;
        }

        return -1;
    }

    public void LoadData(ContainerData data)
    {
        if (data.Items != null)
        {
            for (int i = 0; i < data.Items.Length; i++)
                _items[i].LoadData(data.Items[i]);
        }
    }

    public void LoadData(ColorsData data)
    {
        if (data.Colors != null)
        {
            int index = 0;

            for (int i = 0; i < _items.Count; i++)
            {
                Container car = _items[i].GetComponentInChildren<Container>();

                for (int j = 0; j < car.Items.Count; j++)
                {
                    car.GetItem(j).LoadData(data.Colors[index++]);
                }
            }
        }
    }

    public void DeleteAllExcept(Product product)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] != product)
                Destroy(_items[i].gameObject);
        }
    }

    private void Initialize()
    {
        _items = new List<Product>();

        for (int i = 0; i < _prefabs.Count; i++)
        {
            var item = Instantiate(_prefabs[i]);

            item.gameObject.SetActive(i == 0 && item.GetComponent<CarColor>() && FindFirstObjectByType<StartScreen>());
            item.transform.SetParent(transform, false);

            _items.Add(item);
        }
    }
}