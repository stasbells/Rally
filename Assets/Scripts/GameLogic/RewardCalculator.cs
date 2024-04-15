using TMPro;
using UnityEngine;

public class RewardCalculator : MonoBehaviour
{
    [SerializeField] private TMP_Text _rewardForWinningText;
    [SerializeField] private TMP_Text _rewardForTimeText;

    [SerializeField] private Level _level;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LapCounter _counter;
    [SerializeField] private Stopwatch _stopwatch;

    [SerializeField] private float _rewardIndex;

    private const int MaxRewardForTime = 100;
    private const int RewardForWinning = 50;
    private const int MinReward = 0;

    private void OnEnable() => _counter.Finised += GetReward;

    private void OnDisable() => _counter.Finised -= GetReward;

    private void GetReward()
    {
        _wallet.GetReward(GetRewardForTime() + GetRewardForWinning(_level.GetFinishResault()));

        _rewardForTimeText.text = GetRewardForTime().ToString();
        _rewardForWinningText.text = GetRewardForWinning(_level.GetFinishResault()).ToString();
    }

    private int GetRewardForTime() => Mathf.Clamp(MaxRewardForTime - (int)(_stopwatch.Resault / _rewardIndex), MinReward, MaxRewardForTime);

    private int GetRewardForWinning(bool value) => value ? RewardForWinning : MinReward;
}