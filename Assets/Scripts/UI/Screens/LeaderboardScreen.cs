using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

public class LeaderboardScreen : Screen
{
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private LeaderboardYG _leaderboard;

    public event UnityAction BackToMenuButtonClick;

    public void OnMenuButtonClick()
    {
        BackToMenuButtonClick?.Invoke();
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1f;
        SetInteractable(true);
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0f;
        SetInteractable(false);
    }

    protected override void SetInteractable(bool value)
    {
        _backToMenuButton.interactable = value;
        _backToMenuButton.image.raycastTarget = value;

        _leaderboard.gameObject.SetActive(value);
    }
}