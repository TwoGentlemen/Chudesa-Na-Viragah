using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer(PlayerDataSO data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "player.tw";

        FileStream stream = new FileStream(path,FileMode.Create);

        SaveData saveData = new SaveData(data);

        formatter.Serialize(stream,saveData);
        stream.Close();

        Debug.Log("Save !");
    }

    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "player.tw";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
}
