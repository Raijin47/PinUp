using System;
using UnityEngine;
using Newtonsoft.Json;

public class SaveService
{
    public event Action OnSavesLoaded;
    public Data Saves = new();

    private bool _isReady;
    private readonly string SaveData = "SavesData";

    public bool IsReady
    {
        get => _isReady;
        set
        {
            _isReady = value;
            OnSavesLoaded?.Invoke();
        }
    }

    public void LoadingData()
    {
        string jsonData = PlayerPrefs.GetString(SaveData, string.Empty);

        if (!string.IsNullOrEmpty(jsonData))
        {
            Saves = JsonConvert.DeserializeObject<Data>(jsonData);
        }

        IsReady = true;
    }

    public void SaveProgress()
    {
        string jsonData = JsonConvert.SerializeObject(Saves);
        PlayerPrefs.SetString(SaveData, jsonData);
    }
}