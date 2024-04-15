using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Game _game;

    public bool IsDone { get; private set; } = false;

    public void SetCountdown() => IsDone = true;

    public void ResetCountdown() => IsDone = false;
}