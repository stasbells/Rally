using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FinishScreen : Screen
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private Image _winnerBoard;
    [SerializeField] private Image _loserBoard;
    [SerializeField] private Level _level;

    public event UnityAction RestartButtonClick;

    private void OnEnable()
    {
        _level.Won += ShowWinnerBord;
        _level.Lost += ShowLoserBoard;
    }

    private void OnDisable()
    {
        _level.Won -= ShowWinnerBord;
        _level.Lost -= ShowLoserBoard;
    }

    public void OnRestartButtonClick()
    {
        RestartButtonClick?.Invoke();
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
        _rewardButton.interactable = value;
        _rewardButton.image.raycastTarget = value;

        _restartButton.interactable = value;
        _restartButton.image.raycastTarget = value;

        _winnerBoard.gameObject.SetActive(false);
        _loserBoard.gameObject.SetActive(false);
    }

    private void ShowWinnerBord()
    {
        _winnerBoard.gameObject.SetActive(true);
    }

    private void ShowLoserBoard()
    {
        _loserBoard.gameObject.SetActive(true);
    }
}