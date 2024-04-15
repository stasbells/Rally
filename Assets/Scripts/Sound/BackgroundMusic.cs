using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
        // Воспроизводим музыку
        audioSource.Play();
        // Устанавливаем, чтобы музыка не прерывалась при переключении сцен
        DontDestroyOnLoad(gameObject);
    }
}