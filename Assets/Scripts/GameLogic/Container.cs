using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    [SerializeField] private List<Product> _prefabs;
    [SerializeField] private Wallet _wallet;

    private List<Product> _items;

    public int ItemsCount => _prefabs.Count;
    public int WalletCount => _wallet.Money;
    public IReadOnlyList<Product> Items => _items;

    private void Awake()
    {
        if (_items == null)
            Initialize();
    }

    private void Initialize()
    {
        _items = new List<Product>();

        for (int i = 0; i < _prefabs.Count; i++)
        {
            var item = Instantiate(_prefabs[i]);

            item.gameObject.SetActive(false);
            item.transform.SetParent(transform, false);

            _items.Add(item);
        }
    }

    public Product GetItem(int index) => _items[index];

    public void BuyItem(Product item)
    {
        item.SetBuyed();
        _wallet.Pay(item.Price);
    }

    public void LoadData(ContainerData data)
    {
        if(data.Items != null)
        {
            for (int i = 0; i < data.Items.Length; i++)
                _items[i].LoadData(data.Items[i]);
        }
    }
}