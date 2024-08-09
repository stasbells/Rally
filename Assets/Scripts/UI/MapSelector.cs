using UnityEngine;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{
    [SerializeField] private Screen _screen;
    [SerializeField] private Image _imageLock;
    [SerializeField] private Container _mapStorage;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;

    private int _currentIndex;
    private Map _currentMap;

    private void Awake()
    {
        SelectMap(0);
    }

    private void OnEnable()
    {
        SelectMap(_currentIndex);
    }

    private void OnDisable()
    {
        for (int i = 0; i < _mapStorage.ItemsCount; i++)
            _mapStorage.GetItem(i).gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_currentMap.IsBuyed)
            _imageLock.gameObject.SetActive(false);
    }

    private void SelectMap(int index)
    {
        _nextButton.interactable = (index != _mapStorage.ItemsCount - 1);
        _prevButton.interactable = (index != 0);

        for (int i = 0; i < _mapStorage.ItemsCount; i++)
            _mapStorage.GetItem(i).gameObject.SetActive(i == index);

        _currentMap = _mapStorage.GetItem(index).GetComponent<Map>();

        if (_screen.GetComponent<CanvasGroup>().alpha == 1f)
            ShowInfo();
    }

    public void ChangeMap(int changer)
    {
        _currentIndex += changer;
        SelectMap(_currentIndex);
    }

    private void ShowInfo() => _imageLock.gameObject.SetActive(!_currentMap.IsBuyed);

    public Map GetCurrentMap() => _currentMap;

    public void PayMap() => _mapStorage.BuyItem(_currentMap);
}