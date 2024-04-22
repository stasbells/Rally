using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OverheatView : MonoBehaviour
{
    [SerializeField] private Image _overheatingImage;

    private const float ResetValue = 0f;
    private const float IncreaseTime = 0.8f;
    private const float DecreaseTime = 1.5f;

    private Coroutine _colorChanger;

    public void ChangeColorAlphaTo(float targetAlpha)
    {
        if (_colorChanger != null)
            StopCoroutine(_colorChanger);

        _colorChanger = StartCoroutine(ChangeTo(targetAlpha));
    }

    private IEnumerator ChangeTo(float targetAlpha)
    {
        Color alpha = _overheatingImage.color;
        float stepTime = targetAlpha == ResetValue ? DecreaseTime : IncreaseTime;

        while (alpha.a != targetAlpha)
        {
            alpha.a = Mathf.MoveTowards(alpha.a, targetAlpha, stepTime * Time.deltaTime);
            _overheatingImage.color = alpha;

            yield return null;
        }
    }

    public void ResetColor()
    {
        Color alpha = _overheatingImage.color;
        alpha.a = ResetValue;
        _overheatingImage.color = alpha;
    }
}