using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarEngineSound : MonoBehaviour
{
    [SerializeField] private AudioSource _engineSound;

    private float _minPitch = 0.2f;
    private float _maxPitch = 1f;
    private KeyCode _accelerateKey = KeyCode.W;

    private void Start()
    {
        _engineSound = GetComponent<AudioSource>();
        _engineSound.pitch = _minPitch;
    }

    private void Update()
    {
        if (Input.GetKey(_accelerateKey))
            _engineSound.pitch = Mathf.Lerp(_engineSound.pitch, _maxPitch, Time.deltaTime);
        else
            _engineSound.pitch = Mathf.Lerp(_engineSound.pitch, _minPitch, Time.deltaTime);
    }
}