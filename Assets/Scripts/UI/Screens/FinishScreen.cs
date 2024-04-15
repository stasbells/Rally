using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FinishScreen : Screen 
{
    [SerializeField] private Image _winnerBoard;
    [SerializeField] private Image _loserBoard;

    private Level _level;

    public event UnityAction RestartButtonClick;

    private void OnEnable()
    {
        _level.Won += ShowWinnerBord;
        _level.Lost += ShowLoserBoard;
        Button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _level.Won -= ShowWinnerBord;
        _level.Lost -= ShowLoserBoard;
        Button.onClick.RemoveListener(OnButtonClick);
    }

    private void Awake()
    {
        _level = FindFirstObjectByType<Level>();
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0f;
        Button.interactable = false;
        Button.image.raycastTarget = false;

        _winnerBoard.gameObject.SetActive(false);
        _loserBoard.gameObject.SetActive(false);
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1f;
        Button.interactable = true;
        Button.image.raycastTarget = true;
    }

    protected override void OnButtonClick()
    {
        RestartButtonClick?.Invoke();
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