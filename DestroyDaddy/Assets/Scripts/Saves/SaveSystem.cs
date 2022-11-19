using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save(PlayerData pd) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/DD.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, pd);
        stream.Close();
    }

    public static PlayerData Load() {
        string path = Application.persistentDataPath + "/DD.data";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData pd = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return pd;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
