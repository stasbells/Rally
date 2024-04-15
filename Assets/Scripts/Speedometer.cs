using UnityEngine;
using TMPro;
using System;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private RectTransform _arrow;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _start;

    private const float MaxSpeed = 76f;

    private AnimateCarAlongSpline _target;

    void Start()
    {
        _arrow.localRotation = Quaternion.Euler(0, 0, _start);
    }

    void Update()
    {
        if (_target == null)
            return;

        _text.text = Convert.ToInt32(_target.CurrentSpeed).ToString();

        _arrow.localRotation = Quaternion.Euler(0, 0, GetPosition());
    }

    private float GetPosition()
    {
        float maxSpeed = MaxSpeed / _target.GetComponent<PlayerSpeedController>().MaxSpeed;

        return _start - _target.CurrentSpeed * maxSpeed;
    }

    public void SetTarget(AnimateCarAlongSpline target)
    {
        _target = target;
    }
}