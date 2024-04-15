using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        // �������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();
        // ������������� ������
        audioSource.Play();
        // �������������, ����� ������ �� ����������� ��� ������������ ����
        DontDestroyOnLoad(gameObject);
    }
}