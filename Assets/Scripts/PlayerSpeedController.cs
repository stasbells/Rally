using System;
using UnityEngine;

public class PlayerSpeedController : MonoBehaviour, ISpeedController
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;

    private GameObject _image;

    private const float MinSpeed = 0f;
    private const float MaxSpeedFactor = 0.2f;
    private const float OverheatingFactor = 1.5f;
    private const float FinishFactor = 2f;

    private float _overheatingSpeedValue;
    private float _counter = 0f;
    private bool _isOverheating = false;
    private bool _isFinished = false;

    private void Awake()
    {
        _overheatingSpeedValue = 0.8f * _maxSpeed;
    }

    private void OnDisable()
    {
        _isFinished = false;
    }

    public float MaxSpeed => _maxSpeed;

    public float Change(float speed)
    {
        if (_isFinished)
        {
            _image.SetActive(false);
            return Decrease(speed, FinishFactor);
        }

        if (speed < _overheatingSpeedValue)
            _counter = 0f;

        if (speed < 5f)
            _isOverheating = false;

        if(_counter > 0.5f)
            _image.SetActive(true);

        if(_counter == 0f)
            _image.SetActive(false);

        if (Input.GetKey(KeyCode.W))
        {
            if (speed >= _overheatingSpeedValue && !_isOverheating)
            {
                _counter += Time.deltaTime;

                if (_counter > 3f)
                    _isOverheating = true;

                return Increase(speed, MaxSpeedFactor);
            }
            else
            {
                if (_isOverheating)
                    return Decrease(speed, OverheatingFactor);
                else
                    return Increase(speed);
            }
        }
        else
        {
            return Decrease(speed);
        }
    }

    public void SetFinished() => _isFinished = true;

    public void SetImage(GameObject image) => _image = image;

    private float Increase(float speed, float factor = 1f) => 
        Math.Clamp(speed + _acceleration * factor * Time.deltaTime, MinSpeed, _maxSpeed);

    private float Decrease(float speed, float factor = 1f) => 
        Math.Clamp(speed - _acceleration * factor * Time.deltaTime, MinSpeed, _maxSpeed);
}