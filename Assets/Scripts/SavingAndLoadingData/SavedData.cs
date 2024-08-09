using UnityEngine;
using YG;

public class SavedData : MonoBehaviour
{
    [Header("SavedData")]
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Container _garage;
    [SerializeField] private Container _maps;

    //[Header("Save Config")]
    //[SerializeField] private string _savePath;
    //[SerializeField] private string _saveFileName = "data.json";

    private void Awake()
    {
        //#if UNITY_ANDROID && !UNITY_EDITOR
        // _savePath = Path.Combine(Application.persistentDataPath, _saveFileName);
        //#else
        //_savePath = Path.Combine(Application.dataPath, _saveFileName);
        //#endif

        if (YandexGame.SDKEnabled == true)
            GetLoad();
    }

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (Application.platform == RuntimePlatform.Android)
            Save();
    }

    public void Save()
    {
        SavedDataStruct savedData = new()
        {
            WalletData = new WalletData(_wallet),
            GarageData = new ContainerData(_garage.Items),
            MapData = new ContainerData(_maps.Items),
            ColorsData = new ColorsData(_garage.Items)
        };

        string jsonSavedData = JsonUtility.ToJson(savedData, prettyPrint: true);

        //File.WriteAllText(_savePath, contents: jsonSavedData);

        YandexGame.savesData.jsonSavedData = jsonSavedData;

        YandexGame.SaveProgress();
    }

    public void GetLoad()
    {
        //if (!File.Exists(_savePath))
        //return;

        //string jsonSavedData = File.ReadAllText(_savePath);

        if (YandexGame.savesData.jsonSavedData == null)
            return;

        string jsonSavedData = YandexGame.savesData.jsonSavedData;

        SavedDataStruct savedDataFromJson = JsonUtility.FromJson<SavedDataStruct>(jsonSavedData);

        _wallet.LoadData(savedDataFromJson.WalletData);
        _garage.LoadData(savedDataFromJson.GarageData);
        _maps.LoadData(savedDataFromJson.MapData);
        _garage.LoadData(savedDataFromJson.ColorsData);
    }
}