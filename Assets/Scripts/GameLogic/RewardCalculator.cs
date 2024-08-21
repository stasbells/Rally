using TMPro;
using UnityEngine;
using YG;

public class RewardCalculator : MonoBehaviour
{
    [SerializeField] private TMP_Text _rewardForWinningText;
    [SerializeField] private TMP_Text _rewardForTimeText;

    [SerializeField] private LevelCollector _level;
    [SerializeField] private LapCounter _counter;
    [SerializeField] private Stopwatch _stopwatch;
    [SerializeField] private Wallet _wallet;

    [SerializeField] private float _rewardIndex;

    private const int MaxRewardForTime = 100;
    private const int RewardForWinning = 50;
    private const int MinReward = 0;

    private int _reward;

    private void OnEnable()
    {
        _counter.Finised += GetReward;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        _counter.Finised -= GetReward;
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    private void GetReward()
    {
        _reward = GetRewardForTime() + GetRewardForWinning(_level.GetFinishResault());
        _wallet.GetReward(_reward);

        _rewardForTimeText.text = GetRewardForTime().ToString();
        _rewardForWinningText.text = GetRewardForWinning(_level.GetFinishResault()).ToString();

        YandexGame.NewLBScoreTimeConvert("bestTimeLeaderboard2", (float)_stopwatch.Resault);
    }

    private void Rewarded(int id)
    {
        if (id == 1)
            _wallet.GetReward(_reward);
    }

    public void ExampleOpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    private int GetRewardForTime() => Mathf.Clamp(MaxRewardForTime - (int)(_stopwatch.Resault / _rewardIndex), MinReward, MaxRewardForTime);

    private int GetRewardForWinning(bool value) => value ? RewardForWinning : MinReward;
}