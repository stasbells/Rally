using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ���������� ��� �������� ��������� ����� (���/����)
    public bool isMuted = false;

    // ������� ��� ������������ ��������� �����
    public void ToggleSound()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0 : 1;
    }
}