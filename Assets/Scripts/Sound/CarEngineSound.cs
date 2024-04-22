using UnityEngine;

[RequireComponent(typeof(AnimateCarAlongSpline))]
[RequireComponent(typeof(AudioSource))]
public class CarEngineSound : MonoBehaviour
{
    [SerializeField] private AudioSource _engineSound;

    private AnimateCarAlongSpline _car;

    private void Start()
    {
        _car = GetComponent<AnimateCarAlongSpline>();
        _engineSound = GetComponent<AudioSource>();
        _engineSound.pitch = 0f;
    }

    private void Update()
    {
        if (_car.isActiveAndEnabled)
            _engineSound.pitch = Mathf.Lerp(_engineSound.pitch, (_car.CurrentSpeed + 15f) / 70f, Time.deltaTime * 5f);
    }
}