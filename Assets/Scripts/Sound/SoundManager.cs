using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Переменная для хранения состояния звука (вкл/выкл)
    public bool isMuted = false;

    // Функция для переключения состояния звука
    public void ToggleSound()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
    }
}