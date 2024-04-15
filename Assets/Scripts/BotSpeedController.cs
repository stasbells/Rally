using System;
using UnityEngine;

public class BotSpeedController : MonoBehaviour, ISpeedController
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;

    private const float MinSpeed = 10;
    private const float SpeedLimit = 1.4f;
    private const float IncreaseFactor = 2f;
    private const float MaxOvertake = 0.008f;
    private const float MaxRetard = -0.004f;

    private AnimateCarAlongSpline _targetCar;
    private AnimateCarAlongSpline _botCar;

    public float MaxSpeed => _maxSpeed;

    private void Start()
    {
        _botCar = GetComponent<AnimateCarAlongSpline>();
    }

    public void SetTarget(AnimateCarAlongSpline target)
    {
        _targetCar = target;
    }

    public float Change(float speed)
    {
        if (Distance() > MaxOvertake)
            return Decrease(speed);

        if (Distance() < MaxRetard)
            return Increase(speed, IncreaseFactor, SpeedLimit);

        return Increase(speed);
    }

    private float Distance() => _botCar.TotalDistance - _targetCar.TotalDistance;

    private float Increase(float speed, float factor = 1f, float speedLimit = 1f) => Math.Clamp(speed + _acceleration * factor * Time.deltaTime, MinSpeed, _maxSpeed * speedLimit);

    private float Decrease(float speed, float factor = 1f) => Math.Clamp(speed - _acceleration * factor * Time.deltaTime, MinSpeed, _maxSpeed);
}