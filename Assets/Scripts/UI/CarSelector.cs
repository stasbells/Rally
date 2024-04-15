using UnityEngine;
using UnityEngine.UI;

public class CarSelector : MonoBehaviour
{
    [SerializeField] Screen _screen;
    [SerializeField] Image _imageLock;
    [SerializeField] Container _garage;
    [SerializeField] Button _nextButton;
    [SerializeField] Button _prevButton;

    private int _currentCarIndex;
    private Car _currentCar;

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

        if (_screen.GetComponent<CanvasGroup>().alpha == 1f)
            ShowInfo();
    }

    public void ChangeCar(int change)
    {
        _currentCarIndex += change;
        SelectCar(_currentCarIndex);
    }

    private void ShowInfo() => _imageLock.gameObject.SetActive(!_currentCar.IsBuyed);

    public Car GetCurrentCar() => _currentCar;

    public void PayCar() => _garage.BuyItem(_currentCar);
}