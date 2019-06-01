using Hel.SavingLoading;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

public class GameSaveHandler : SerializedMonoBehaviour
{
    [Required] [SerializeField] private List<ISaveable> scriptableObjectsToSave = new List<ISaveable>();

    public static string GetSaveDataPath() { return $"{Application.persistentDataPath}/save_data"; }

    private void Awake()
    {
        LoadGame();
    }

    [Button]
    public void SaveGame()
    {
        if (!Directory.Exists(GetSaveDataPath())) { Directory.CreateDirectory(GetSaveDataPath()); }

        List<ISaveable> saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToList();

        saveables.AddRange(scriptableObjectsToSave);

        foreach (ISaveable saveable in saveables)
        {
            saveable.Save();
        }
    }

    [Button]
    public void LoadGame()
    {
        if (!Directory.Exists(GetSaveDataPath())) { return; }

        List<ISaveable> loadables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToList();

        loadables.AddRange(scriptableObjectsToSave);

        var orderedLoadables = loadables.OrderByDescending(i => i.LoadPriority);

        foreach (ISaveable loadable in orderedLoadables)
        {
            loadable.Load();
        }
    }

    public static void SaveFile(string path, object objectToSave)
    {
        try
        {
            string json = JsonUtility.ToJson(objectToSave, true);
            File.WriteAllText($"{GetSaveDataPath()}/{path}.json", json);
        }
        catch (SerializationException e)
        {
            Debug.LogError($"Error Serializing Data: {e.Message}");
        }
    }

    public static bool LoadFile(string path, object objectToLoadTo)
    {
        try
        {
            if (!File.Exists($"{GetSaveDataPath()}/{path}.json")) { return false; }
            string json = File.ReadAllText($"{GetSaveDataPath()}/{path}.json");
            JsonUtility.FromJsonOverwrite(json, objectToLoadTo);
        }
        catch (SerializationException e)
        {
            Debug.LogError($"Error Deserializing Data: {e.Message}");
            return false;
        }
        return true;
    }

    public static void RemoveFile(string path) => File.Delete($"{GetSaveDataPath()}/{path}.json");
}
