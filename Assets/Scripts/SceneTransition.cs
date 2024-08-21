using Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private TMP_Text _loadingPresentage;
    [SerializeField] private Image _loadingProgressBar;

    private static SceneTransition Instance;

    private AsyncOperation _loadingSceneOperation;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        Instance = this;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (_loadingSceneOperation != null)
        {
            _loadingPresentage.text = Mathf.RoundToInt(_loadingSceneOperation.progress * 100).ToString() + "%";
            _loadingProgressBar.fillAmount = Mathf.Lerp(_loadingProgressBar.fillAmount, _loadingSceneOperation.progress, Time.deltaTime * 5);
        }
    }

    public static void SwitchToScene(string sceneName)
    {
        Instance._canvasGroup.alpha = 1f;
        Instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
    }

    public void GoToGame()
    {
        SwitchToScene(SceneUtility.GetSceneNameByBuildIndex(Constants.GameSceneIndex));
    }

    public void GoToMenu()
    {
        SwitchToScene(SceneUtility.GetSceneNameByBuildIndex(Constants.MenuSceneIndex));
    }
}