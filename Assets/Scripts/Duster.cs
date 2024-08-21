using UnityEngine;

public class Duster : MonoBehaviour
{
    [SerializeField] private ParticleSystem _rightWheel;
    [SerializeField] private ParticleSystem _leftWheel;

    private void OnEnable()
    {
        if (FindFirstObjectByType<StartScreen>())
            OnStop();
        else
            OnPlay();
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