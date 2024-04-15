using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuScreen : Screen
{
    [SerializeField] private Button _gargeMenuButton;
    [SerializeField] private Button _settingsMenuButton;

    public event UnityAction PlayButtonClick;
    public event UnityAction GarageButtonClick;
    public event UnityAction SettingsButtonClick;

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
        PlayButtonClick?.Invoke();
    }

    public void OnGarageButtonClick()
    {
        GarageButtonClick?.Invoke();
    }

    public void OnSettingsButtonClick()
    {
        SettingsButtonClick?.Invoke();
    }

    public void SetInterectable(bool value)
    {
        Button.interactable = value;
        Button.image.raycastTarget = value;
        _gargeMenuButton.interactable = value;
        _gargeMenuButton.image.raycastTarget = value;
        _settingsMenuButton.interactable = value;
        _settingsMenuButton.image.raycastTarget = value;
    }
}