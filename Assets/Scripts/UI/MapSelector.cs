using UnityEngine;
using UnityEngine.UI;
using YG;

public class MapSelector : MonoBehaviour
{
    [SerializeField] private Screen _screen;
    [SerializeField] private Image _imageLock;
    [SerializeField] private Container _mapStorage;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Image _image;

    private Map _currentMap;
    private int _currentMapIndex;

    private void OnEnable()
    {
        SelectMap(_currentMapIndex);

        SaveCurrentMapIndex();
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

        //for (int i = 0; i < _mapStorage.ItemsCount; i++)
            //_mapStorage.GetItem(i).gameObject.SetActive(i == index);

        _currentMap = _mapStorage.GetItem(index).GetComponent<Map>();
        _image.sprite = _currentMap.GetComponentInChildren<SpriteRenderer>().sprite;

        SaveCurrentMapIndex();

        if (_screen.GetComponent<CanvasGroup>().alpha == 1f)
            ShowInfo();
    }

    public void ChangeMap(int changer)
    {
        _currentMapIndex += changer;
        SelectMap(_currentMapIndex);
    }

    private void SaveCurrentMapIndex() => YandexGame.savesData.MapIndex = _currentMapIndex;

    public Map GetCurrentMap() => _currentMap;

    public void PayMap() => _mapStorage.BuyItem(_currentMap);

    private void ShowInfo() => _imageLock.gameObject.SetActive(!_currentMap.IsBuyed);
}