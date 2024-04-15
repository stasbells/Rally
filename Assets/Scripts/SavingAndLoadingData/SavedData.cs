using UnityEngine;
using System.IO;

public class SavedData : MonoBehaviour
{
    [Header("Money")]
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Container _garage;
    [SerializeField] private Container _maps;

    [Header("Save Config")]
    [SerializeField] private string _savePath;
    [SerializeField] private string _saveFileName = "data.json";

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _savePath = Path.Combine(Application.persistentDataPath, _saveFileName);
#else
        _savePath = Path.Combine(Application.dataPath, _saveFileName);
#endif
        LoadFromFile();
    }

    private void OnApplicationQuit()
    {
        SaveToFile();
    }

    private void OnApplicationPause(bool pause)
    {
        if (Application.platform == RuntimePlatform.Android)
            SaveToFile();
    }

    public void SaveToFile()
    {
        SavedDataStruct savedData = new()
        {
            WalletData = new WalletData(_wallet),
            GarageData = new ContainerData(_garage.Items),
            MapData = new ContainerData(_maps.Items)
        };

        string json = JsonUtility.ToJson(savedData, prettyPrint: true);

        File.WriteAllText(_savePath, contents: json);
    }

    public void LoadFromFile()
    {
        if (!File.Exists(_savePath))
            return;

        string json = File.ReadAllText(_savePath);

        SavedDataStruct savedDataFromJson = JsonUtility.FromJson<SavedDataStruct>(json);

        _wallet.LoadData(savedDataFromJson.WalletData);
        _garage.LoadData(savedDataFromJson.GarageData);
        _maps.LoadData(savedDataFromJson.MapData);
    }
}