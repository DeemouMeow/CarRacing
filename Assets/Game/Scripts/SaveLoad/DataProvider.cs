using Newtonsoft.Json;
using System.IO;
using System;

public class DataProvider
{
    private readonly string k_persistentDataPath = UnityEngine.Application.persistentDataPath;
    private readonly string k_saveFileName = "SaveData";
    private readonly string k_baseFileExtention = ".json";

    public void Save<T>(T data)
    {
        var settings = new JsonSerializerSettings() { Formatting = Formatting.Indented, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        string serializedData = JsonConvert.SerializeObject(data, settings);

        string savePath = Path.Combine(k_persistentDataPath, k_saveFileName + k_baseFileExtention);
        File.WriteAllText(savePath, serializedData);
    }

    public T TryLoad<T>(Func<T> defaultCreateFunction)
    {
        string savePath = Path.Combine(k_persistentDataPath, k_saveFileName + k_baseFileExtention);

        if (!File.Exists(savePath))
            return defaultCreateFunction();

        string savedData = File.ReadAllText(savePath);
        T data = JsonConvert.DeserializeObject<T>(savedData);

        return data;
    }
}
