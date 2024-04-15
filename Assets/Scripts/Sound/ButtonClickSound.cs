using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private AudioSource _buttonClickSound;
    [SerializeField] private Camera _camera;

    void Awake()
    {
        _buttonClickSound.playOnAwake = false;
    }

    public void PlaySound(Button button)
    {
        _buttonClickSound.Play();
    }
}