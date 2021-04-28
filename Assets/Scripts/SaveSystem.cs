using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveData(PlayerDataSO playerData)
    {
        string path = Application.persistentDataPath + "player.tw";
        BinaryFormatter formatter = new BinaryFormatter();

        Data data = new Data(playerData);
        
        using(FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream,data);

        }
    }

    public static Data LoadData()
    {
        string path = Application.persistentDataPath + "player.tw";
        BinaryFormatter formatter = new BinaryFormatter();

        if (!File.Exists(path)) { return null;}

        using(FileStream stream = new FileStream(path, FileMode.Open))
        {

            return formatter.Deserialize(stream) as Data;
        }
    }
}
