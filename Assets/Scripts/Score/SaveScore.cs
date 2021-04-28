using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveScore
{
    public static string[] Name = new string[10];
    public static string[] Score = new string[10];
    public static int playerIndex = 0;


    private static int convertDeSec(string str)
    {
        var s = str.Split(':');
        int res = 0;

        res+=int.Parse(s[0])*60*60;
        res+=int.Parse(s[1])*60;
        res+=int.Parse(s[2]);

        return res;
        
    }

    private static void Sort()
    {
        for (int i = 0; i < playerIndex-1; i++)
        {
            for (int j = i+1; j < playerIndex; j++)
            {
                if(convertDeSec(Score[i]) < convertDeSec(Score[j])){

                    var buf = Score[i];
                    var buf2 = Name[i];

                    Score[i] = Score[j];
                    Name[i] = Name[j];

                    Score[j] = buf;
                    Name[j] = buf2;
                }
            }
        }
    }

    public static void AddPlayer(string name, string score)
    {

        if (playerIndex >= Name.Length)
        {
            Sort();

            if (convertDeSec(Score[playerIndex - 1]) > convertDeSec(score))
            {
                Score[playerIndex - 1] = score;
                Name[playerIndex-1] = name;

                Sort();
                SaveDataScore();
            }

            return; 
        }

        Name[playerIndex] = name;
        Score[playerIndex] = score;

        playerIndex++;
        Debug.Log("Add player!");
        SaveDataScore();

    }

    public static void ClearData()
    {

        for (int i = 0; i < Name.Length; i++)
        {
            Name[i] = "";
            Score[i] = "";
        }
        playerIndex = 0;
        Debug.Log("Data clear!");
    }
    public static void LoadDataScore()
    {
        string path = Application.persistentDataPath + "/score.red";
        BinaryFormatter formatter = new BinaryFormatter();

        if (!File.Exists(path)) { return; }

        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            PlayersData data = formatter.Deserialize(stream) as PlayersData;
            Debug.Log("Load Score");
            Name =  data.name;
            Score = data.score;
            playerIndex = data.indexPlayer;
        }
    }

    public static void SaveDataScore()
    {
        if (playerIndex == 0) { return; }

        string path = Application.persistentDataPath + "/score.red";
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            PlayersData data = new PlayersData(Name,Score,playerIndex);
            formatter.Serialize(stream, data);
            Debug.Log("Save Score");
        }
    }
}
