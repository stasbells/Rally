using TMPro;
using UnityEngine;

public class CarStatsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _maxSpeedText;
    [SerializeField] private TMP_Text _accelerationText;
    [SerializeField] private TMP_Text _overheatingTimeText;

    [SerializeField] private CarSelector _selector;

    private PlayerSpeedController _car;

    private void Start()
    {
        ShowStats();
    }

    public void ShowStats()
    {
        _car = _selector.GetCurrentProduct().GetComponent<PlayerSpeedController>();

        _maxSpeedText.text = _car.MaxSpeed.ToString();
        _accelerationText.text = _car.Acceleration.ToString();
        _overheatingTimeText.text = _car.OverheatingTime.ToString();
    }
}