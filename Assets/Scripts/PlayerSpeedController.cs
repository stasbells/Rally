using System;
using UnityEngine;

public class PlayerSpeedController : MonoBehaviour, ISpeedController
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;

    private OverheatWarning _overheatWarning;
    private PlayerInput _playerInput;

    private const float MinSpeed = 0f;
    private const float MaxSpeedFactor = 0.2f;
    private const float OverheatingFactor = 1.5f;

    private float _counter = 0f;
    private float _overheatingSpeedValue = 0.8f;
    private bool _isOverheating = false;

    public float MaxSpeed => _maxSpeed;

    private void Awake()
    {
        _overheatingSpeedValue *= _maxSpeed;
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _counter = 0f;
        _playerInput.Disable();
    }

    public float Change(float speed)
    {
        if (speed < _overheatingSpeedValue)
            _counter = 0f;

        if (speed < 5f)
            _isOverheating = false;

        _overheatWarning.ActiveBy(_counter);

        if (_playerInput.Player.Move.ReadValue<float>() > 0.1f)
        {
            if (speed >= _overheatingSpeedValue && !_isOverheating)
            {
                ÑountUntilOverheating();

                return Increase(speed, MaxSpeedFactor);
            }
            else
            {
                return _isOverheating ? Decrease(speed, OverheatingFactor) : Increase(speed);
            }
        }
        else
        {
            return Decrease(speed);
        }
    }

    private void ÑountUntilOverheating()
    {
        _counter += Time.deltaTime;

        if (_counter > 3f)
            _isOverheating = true;
    }

    public void SetOverheatWarning(OverheatWarning overheatWarning) => _overheatWarning = overheatWarning;

    private float Increase(float speed, float factor = 1f) =>
        Math.Clamp(speed + _acceleration * factor * Time.deltaTime, MinSpeed, _maxSpeed);

    private float Decrease(float speed, float factor = 1f) =>
        Math.Clamp(speed - _acceleration * factor * Time.deltaTime, MinSpeed, _maxSpeed);
}