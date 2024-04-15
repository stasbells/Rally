using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsScreen : Screen
{
    [SerializeField] private Button _laguageButton;

    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Slider _soundSlider;

    public event UnityAction BackToMenuButtonClick;

    public override void Close()
    {
        CanvasGroup.alpha = 0f;
        SetInteractable(false);
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1f;
        SetInteractable(true);
    }

    protected override void OnButtonClick()
    {
        BackToMenuButtonClick?.Invoke();
    }

    private void SetInteractable(bool value)
    {
        Button.interactable = value;
        Button.image.raycastTarget = value;

        _laguageButton.interactable = value;
        _laguageButton.image.raycastTarget = value;

        _volumeSlider.gameObject.SetActive(value);

        _soundSlider.gameObject.SetActive(value);
    }
}