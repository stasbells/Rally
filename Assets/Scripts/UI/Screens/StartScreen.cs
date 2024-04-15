using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartScreen : Screen
{
    [SerializeField] private TMP_Text _moneyCount;
    [SerializeField] private CarSelector _carSelector;
    [SerializeField] private MapSelector _mapSelector;
    [SerializeField] private Level _level;
    [SerializeField] private Button _buyMapButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private Container _garage;
    [SerializeField] private Container _botStorage;

    public event UnityAction StartButtonClick;
    public event UnityAction BackToMenuButtonClick;

    private void Update()
    {
        if (_garage != null)
            _moneyCount.text = _garage.WalletCount.ToString();

        if (CanvasGroup.alpha == 1f)
        {
            SetStartButtonInteracteble(_carSelector.GetCurrentCar().IsBuyed && _mapSelector.GetCurrentMap().IsBuyed);
            BuyMapButtonView(!_mapSelector.GetCurrentMap().IsBuyed);
        }
    }

    public override void Close()
    {
        _level.SetComponents(_mapSelector.GetCurrentMap(), _carSelector.GetCurrentCar(), _botStorage.GetItem(0).GetComponent<Bot>());

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

    public void BuyMapButtonView(bool value)
    {
        _buyMapButton.gameObject.SetActive(value);
        SetBuyButtonInterectable(_mapSelector.GetCurrentMap().Price <= _garage.WalletCount);

        if (_buyMapButton.gameObject.activeSelf)
            _buyMapButton.gameObject.GetComponentInChildren<TMP_Text>().text = _mapSelector.GetCurrentMap().Price.ToString();
    }

    private void SetStartButtonInteracteble(bool value)
    {
        Button.interactable = value;
        Button.image.raycastTarget = value;
    }

    private void SetBuyButtonInterectable(bool value)
    {
        _buyMapButton.interactable = value;
        _buyMapButton.image.raycastTarget = value;
    }

    private void SetInterectable(bool value)
    {
        SetStartButtonInteracteble(value);

        _backToMenuButton.interactable = value;
        _backToMenuButton.image.raycastTarget = value;

        _carSelector.gameObject.SetActive(value);
        _mapSelector.gameObject.SetActive(value);
    }
}