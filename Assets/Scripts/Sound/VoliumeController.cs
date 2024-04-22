using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private List<AudioSource> _gameMusic;

    void Start()
    {
        //if (PlayerPrefs.HasKey("musicVolume"))
        //{
        //    float savedVolume = PlayerPrefs.GetFloat("musicVolume");
        //    volumeSlider.value = savedVolume;

        //    foreach (AudioSource source in gameMusic)
        //        source.volume = savedVolume;
        //}
        //else
        //{
        foreach (AudioSource source in _gameMusic)
            source.volume = _volumeSlider.value;
        //}
    }

    public void OnVolumeChange()
    {
        foreach (AudioSource source in _gameMusic)
            source.volume = _volumeSlider.value;

        //SaveVolumeSetting();
    }

    //void SaveVolumeSetting()
    //{
    //    PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    //    PlayerPrefs.Save();
    //}
}