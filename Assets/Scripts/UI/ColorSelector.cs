using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour
{
    [SerializeField] private GarageScreen _garageScreen;
    [SerializeField] private Image _imageLock;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;
    [SerializeField] private CarSelector _carSelector;

    private Car _curentCar;
    private CarColor _currentColor;
    private Container _colors;
    private int _currentColorIndex;

    private void Start()
    {
        _curentCar = _carSelector.GetCurrentProduct().GetComponent<Car>();
        _colors = _curentCar.GetComponentInChildren<Container>();
    }

    private void SetCurrentCar(Car car)
    {
        _curentCar = car;
        _colors = _curentCar.GetComponentInChildren<Container>();
        _currentColorIndex = _colors.CurrentItemIndex;
        SelectColor(_currentColorIndex);
    }

    private void OnEnable()
    {
        //_garageScreen.BackToMenuButtonClick += SelectDefaultColor;
        _carSelector.CarChanged += SetCurrentCar;
    }

    private void OnDisable()
    {
        //_garageScreen.BackToMenuButtonClick -= SelectDefaultColor;
        _carSelector.CarChanged -= SetCurrentCar;
    }

    private void Update()
    {
        if (_currentColor.IsBuyed)
            _imageLock.gameObject.SetActive(false);
    }

    private void SelectColor(int index)
    {
        _nextButton.interactable = (index != _colors.ItemsCount - 1);
        _prevButton.interactable = (index != 0);

        for (int i = 0; i < _colors.ItemsCount; i++)
            _colors.GetItem(i).gameObject.SetActive(i == index);

        _currentColor = _colors.GetItem(index).GetComponent<CarColor>();
        _colors.SetCurrentIndex(index);

        if (_garageScreen.GetComponent<CanvasGroup>().alpha == 1f)
            ShowInfo();
    }

    public void SelectDefaultColor()
    {
        int defaultColorIndex = 0;

        if (!_currentColor.IsBuyed)
            SelectColor(defaultColorIndex);
    }

    public void ChangeColor(int changer)
    {
        _currentColorIndex = Mathf.Clamp(_currentColorIndex += changer, 0, _colors.ItemsCount - 1);
        SelectColor(_currentColorIndex);
    }

    public void PayColor() => _colors.BuyItem(_currentColor);

    public Product GetCurrentProduct() => _currentColor;

    private void ShowInfo() => _imageLock.gameObject.SetActive(!_currentColor.IsBuyed);
}