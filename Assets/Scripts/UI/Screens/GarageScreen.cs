using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GarageScreen : Screen
{
    [SerializeField] private TMP_Text _moneyCount;
    [SerializeField] private Container _garage;
    [SerializeField] private CarSelector _carSelector;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private Button _buyCar;

    public event UnityAction StartButtonClick;
    public event UnityAction BackToMenuButtonClick;

    private void Update()
    {
        if (_garage != null)
            _moneyCount.text = _garage.WalletCount.ToString();

        if (CanvasGroup.alpha == 1f)
            BuyCarButtonView(!_carSelector.GetCurrentCar().IsBuyed);
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0f;
        SetInterectable(false);
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1f;
        SetInterectable(true);
    }

    protected override void OnButtonClick()
    {
        StartButtonClick?.Invoke();
    }

    public void OnMenuButtonClick()
    {
        BackToMenuButtonClick?.Invoke();
    }

    private void BuyCarButtonView(bool value)
    {
        _buyCar.gameObject.SetActive(value);
        SetBuyButtonInterectable(_carSelector.GetCurrentCar().Price <= _garage.WalletCount);

        if (_buyCar.gameObject.activeSelf)
            _buyCar.gameObject.GetComponentInChildren<TMP_Text>().text = _carSelector.GetCurrentCar().Price.ToString();
    }

    private void SetBuyButtonInterectable(bool value)
    {
        _buyCar.interactable = value;
        _buyCar.image.raycastTarget = value;
    }

    private void SetInterectable(bool value)
    {
        Button.interactable = value;
        Button.image.raycastTarget = value;

        _buyCar.interactable = value;
        _buyCar.image.raycastTarget = value;

        _backToMenuButton.interactable = value;
        _backToMenuButton.image.raycastTarget = value;

        _carSelector.gameObject.SetActive(value);
    }
}