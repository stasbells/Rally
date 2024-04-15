using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Car _player;
    [SerializeField] private Image _default;
    [SerializeField] private Image _bronze;
    [SerializeField] private Image _silver;
    [SerializeField] private Image _gold;
    [SerializeField] private Image _newRecord;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bestScoreText;

    private int _score;
    private int _bestScore;
    private int _bronceMedalResault = 1;
    private int _silverMedalResault = 3;
    private int _goldMedalResault = 5;

    private void OnEnable()
    {
        //_player.ScoreChanged += ScoreChanged;
    }

    private void OnDisable()
    {
        //_player.ScoreChanged += ScoreChanged;
    }

    private void Start()
    {
        _bestScore = 0;
    }

    private void ScoreChanged(int score)
    {
        _score = score;
        _scoreText.text = _score.ToString(); 

        ResetColor();

        if (_score >= _bestScore)
        {
            _bestScore = _score;
            _bestScoreText.text = _bestScore.ToString();

            _newRecord.color = Color.white;
        }

        if (_score >= _bronceMedalResault)
            _bronze.color = Color.white;

        if (_score >= _silverMedalResault)
            _silver.color = Color.white;

        if (_score >= _goldMedalResault)
            _gold.color = Color.white;
    }

    private void ResetColor()
    {
        _newRecord.color = Color.clear;
        _bronze.color = Color.clear;
        _silver.color = Color.clear;
        _gold.color = Color.clear;
    }
}