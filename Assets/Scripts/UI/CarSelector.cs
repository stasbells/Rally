using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CarSelector : MonoBehaviour
{
    [SerializeField] private Screen _screen;
    [SerializeField] private Image _imageLock;
    [SerializeField] private Container _garage;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;

    private int _currentCarIndex;
    private Car _currentCar;

    public event UnityAction<Car> CarChanged;

    private void Awake()
    {
        SelectCar(0);
    }

    private void OnEnable()
    {
        SelectCar(_currentCarIndex);
    }

    private void OnDisable()
    {
        for (int i = 0; i < _garage.ItemsCount; i++)
            _garage.GetItem(i).gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_currentCar.IsBuyed)
            _imageLock.gameObject.SetActive(false);
    }

    private void SelectCar(int index)
    {
        _nextButton.interactable = (index != _garage.ItemsCount - 1);
        _prevButton.interactable = (index != 0);

        for (int i = 0; i < _garage.ItemsCount; i++)
            _garage.GetItem(i).gameObject.SetActive(i == index);

        _currentCar = _garage.GetItem(index).GetComponent<Car>();

        CarChanged?.Invoke(_currentCar);

        if (_screen.GetComponent<CanvasGroup>().alpha == 1f)
            ShowInfo();
    }

    public void ChangeCar(int change)
    {
        _currentCarIndex += change;
        SelectCar(_currentCarIndex);
    }
    public Product GetCurrentProduct() => _currentCar;

    public void PayCar() => _garage.BuyItem(_currentCar);

    private void ShowInfo() => _imageLock.gameObject.SetActive(!_currentCar.IsBuyed);
}