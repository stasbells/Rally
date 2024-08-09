using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] private MenuScreen _menuScreen;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GarageScreen _garageScreen;
    [SerializeField] private SettingsScreen _settingsScreen;
    [SerializeField] private LeaderboardScreen _leaderboardScreen;
    [SerializeField] private FinishScreen _finishScreen;
    [SerializeField] private LapCounter _lapCounter;

    public event UnityAction GameStartet;
    public event UnityAction Finished;
    public event UnityAction GameRestarted;

    private void OnEnable()
    {
        _menuScreen.PlayButtonClick += OnStartButtonClick;
        _menuScreen.GarageButtonClick += OnGarageButtonClick;
        _menuScreen.SettingsButtonClick += OnSettingsButtonClick;
        _menuScreen.LeaderboardButtonClick += OnLeaderbordButtonClick;
        _startScreen.StartButtonClick += OnPlayButtonClick;
        _startScreen.BackToMenuButtonClick += OnMenuButtonClick;
        _garageScreen.BackToMenuButtonClick += OnMenuButtonClick;
        _settingsScreen.BackToMenuButtonClick += OnMenuButtonClick;
        _leaderboardScreen.BackToMenuButtonClick += OnMenuButtonClick;
        _finishScreen.RestartButtonClick += OnRestartButtonClick;
        _lapCounter.Finised += OnFinish;
    }

    private void OnDisable()
    {
        _menuScreen.PlayButtonClick -= OnStartButtonClick;
        _menuScreen.GarageButtonClick -= OnGarageButtonClick;
        _menuScreen.SettingsButtonClick -= OnSettingsButtonClick;
        _menuScreen.LeaderboardButtonClick -= OnLeaderbordButtonClick;
        _startScreen.StartButtonClick -= OnPlayButtonClick;
        _startScreen.BackToMenuButtonClick -= OnMenuButtonClick;
        _garageScreen.BackToMenuButtonClick -= OnMenuButtonClick;
        _settingsScreen.BackToMenuButtonClick -= OnMenuButtonClick;
        _leaderboardScreen.BackToMenuButtonClick -= OnMenuButtonClick;
        _finishScreen.RestartButtonClick -= OnRestartButtonClick;
        _lapCounter.Finised -= OnFinish;
    }

    private void Start()
    {
        _menuScreen.Open();
        _startScreen.Close();
        _finishScreen.Close();
        _garageScreen.Close();
        _leaderboardScreen.Close();
        _settingsScreen.Close();
    }

    private void OnStartButtonClick()
    {
        _menuScreen.Close();
        _startScreen.Open();
    }

    private void OnMenuButtonClick()
    {
        if (_garageScreen.GetComponent<CanvasGroup>().alpha == 1f)
            _garageScreen.Close();

        if (_startScreen.GetComponent<CanvasGroup>().alpha == 1f)
            _startScreen.Close();

        _settingsScreen.Close();
        _leaderboardScreen.Close();
        _menuScreen.Open();
    }

    private void OnGarageButtonClick()
    {
        _menuScreen.Close();
        _garageScreen.Open();
    }

    private void OnSettingsButtonClick()
    {
        _menuScreen.Close();
        _settingsScreen.Open();
    }

    private void OnLeaderbordButtonClick() 
    {
        _menuScreen.Close();
        _leaderboardScreen.Open();
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        GameStartet?.Invoke();
    }

    public void OnFinish()
    {
        _finishScreen.Open();
        Finished?.Invoke();
    }

    private void OnRestartButtonClick()
    {
        _finishScreen.Close();
        _menuScreen.Open();
        GameRestarted?.Invoke();
    }
}