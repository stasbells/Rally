using UnityEngine;
using UnityEngine.Events;
using YG;

public class Level : MonoBehaviour
{
    [SerializeField] private GameMenu _game;
    [SerializeField] private Stopwatch _stopwatch;
    [SerializeField] private Countdown _countdown;
    [SerializeField] private LapCounter _lapCounter;
    [SerializeField] private Speedometer _speedometer;
    [SerializeField] private OverheatWarning _overheatWarning;
    [SerializeField] private Tutorial _tutorial;

    private AnimateCarAlongSpline _playerCar;
    private AnimateCarAlongSpline _botCar;
    private LoftRoad _map;
    private Follower _follower;

    public bool IsDone => _countdown.IsDone;

    public event UnityAction Won;
    public event UnityAction Lost;

    private void OnEnable()
    {
        _game.GameStartet += Load;
        _game.GameRestarted += Disable;
        _lapCounter.Finised += DisableView;
    }

    private void OnDisable()
    {
        _game.GameStartet -= Load;
        _game.GameRestarted -= Disable;
        _lapCounter.Finised -= DisableView;
    }

    public void SetComponents(Map map, Car playerCar, Bot botCar)
    {
        _map = map.gameObject.GetComponent<LoftRoad>();
        _playerCar = playerCar.gameObject.GetComponent<AnimateCarAlongSpline>();
        _botCar = botCar.gameObject.GetComponent<AnimateCarAlongSpline>();

        _map.gameObject.SetActive(false);
        _playerCar.gameObject.SetActive(false);
        _botCar.gameObject.SetActive(false);
    }

    private void Load()
    {
        _countdown.gameObject.SetActive(true);
        _speedometer.gameObject.SetActive(true);
        _stopwatch.gameObject.SetActive(true);
        _lapCounter.gameObject.SetActive(true);

        _tutorial.gameObject.SetActive(YandexGame.savesData.isFirstSession);

        _stopwatch.TimeStart();

        _playerCar.gameObject.SetActive(true);
        _botCar.gameObject.SetActive(true);
        _map.gameObject.SetActive(true);

        _map.gameObject.GetComponentInChildren<Camera>().enabled = false;
        _playerCar.gameObject.GetComponentInChildren<Camera>().enabled = false;

        _playerCar.SetContainer(_map.GetComponent<LoftRoad>().Container);
        _botCar.SetContainer(_map.GetComponent<LoftRoad>().Container);

        _playerCar.SetCountdown(_countdown);
        _botCar.SetCountdown(_countdown);

        _playerCar.GetComponent<PlayerSpeedController>().SetOverheatWarning(_overheatWarning);

        _botCar.GetComponent<BotSpeedController>().SetTarget(_playerCar);
        _lapCounter.SetTarget(_playerCar);
        _speedometer.SetTarget(_playerCar);

        _follower = Camera.main.GetComponent<Follower>();
        _follower.SetTarget(_playerCar.transform);
    }

    private void DisableView()
    {
        _stopwatch.TimeStop();
        _stopwatch.TimeReset();
        _countdown.ResetCountdown();

        _countdown.gameObject.SetActive(false);
        _speedometer.gameObject.SetActive(false);
        _stopwatch.gameObject.SetActive(false);
        _lapCounter.gameObject.SetActive(false);

        FinishResault()?.Invoke();
    }

    private void Disable()
    {
        _countdown.gameObject.SetActive(false);
        _speedometer.gameObject.SetActive(false);

        _map.gameObject.GetComponentInChildren<Camera>().enabled = true;
        _playerCar.gameObject.GetComponentInChildren<Camera>().enabled = true;

        _map.gameObject.SetActive(false);
        _playerCar.gameObject.SetActive(false);
        _botCar.gameObject.SetActive(false);
    }

    public bool GetFinishResault() => _playerCar.TotalDistance > _botCar.TotalDistance;

    private UnityAction FinishResault() => _ = GetFinishResault() ? Won : Lost;
}