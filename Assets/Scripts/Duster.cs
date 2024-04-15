using UnityEngine;

public class Duster : MonoBehaviour
{
    [SerializeField] private ParticleSystem _rightWheel;
    [SerializeField] private ParticleSystem _leftWheel;

    private Level _level;

    private void Awake()
    {
        _level = FindAnyObjectByType<Level>();
    }

    private void Update()
    {
        if (_level.IsDone && _leftWheel.isStopped)
            OnPlay();
    }

    private void OnEnable()
    {
        if (_leftWheel.isPlaying)
            OnStop();
    }

    private void OnDisable()
    {
        if (_leftWheel.isPlaying)
            OnStop();
    }

    public void OnPlay()
    {
        _leftWheel.Play();
        _rightWheel.Play();
    }

    private void OnStop()
    {
        _leftWheel.Stop();
        _rightWheel.Stop();
    }
}