using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveScore
{
    public const int countLevel = 5;
    public const int maxPlayer = 10;

    public static string[,] Name = new string[countLevel, maxPlayer];
    public static string[,] Score = new string[countLevel, maxPlayer];
    public static int[] playerIndex = new int[countLevel];


    private static int convertDeSec(string str)
    {
        var s = str.Split(':');
        int res = 0;

        res+=int.Parse(s[0])*60*60;
        res+=int.Parse(s[1])*60;
        res+=int.Parse(s[2]);

        return res;
        
    }

    private static void Sort(int levelIndex)
    {
        for (int i = 0; i < playerIndex[levelIndex]-1; i++)
        {
            for (int j = i+1; j < playerIndex[levelIndex]; j++)
            {
                if(convertDeSec(Score[levelIndex,i]) > convertDeSec(Score[levelIndex,j])){

                    var buf = Score[levelIndex,i];
                    var buf2 = Name[levelIndex,i];

                    Score[levelIndex,i] = Score[levelIndex,j];
                    Name[levelIndex,i] = Name[levelIndex,j];

                    Score[levelIndex,j] = buf;
                    Name[levelIndex,j] = buf2;
                }
            }
        }
    }

    public static void AddPlayer(string name, string score,int levelIndex)
    {
        if(playerIndex == null) { Debug.LogError("Error");}

        if(playerIndex[levelIndex] > 0)
        {
            for (int i = 0; i < playerIndex[levelIndex]; i++)
            {
                if(Name[levelIndex,i] == name)
                {
                    if (convertDeSec(Score[levelIndex,i]) > convertDeSec(score))
                    {
                        Score[levelIndex,i] = score;
                        Sort(levelIndex);
                        SaveDataScore();
                        Debug.Log("Player update");
                    }
                    return;
                }
            }
        }

        if (playerIndex[levelIndex] >= maxPlayer)
        {
            Sort(levelIndex);

            if (convertDeSec(Score[levelIndex,playerIndex[levelIndex] - 1]) > convertDeSec(score))
            {
                Score[levelIndex,playerIndex[levelIndex] - 1] = score;
                Name[levelIndex,playerIndex[levelIndex]-1] = name;

                Sort(levelIndex);
                SaveDataScore();
            }

            return; 
        }

        Name[levelIndex, playerIndex[levelIndex] ] = name;
        Score[levelIndex,playerIndex[levelIndex] ] = score;

        playerIndex[levelIndex]++;
        Debug.Log("Add player!");
        Sort(levelIndex);
        SaveDataScore();

    }

    public static void ClearData(int levelIndex)
    {

        for (int i = 0; i < maxPlayer; i++)
        {
            Name[levelIndex,i] = "";
            Score[levelIndex,i] = "";
        }
        playerIndex[levelIndex] = 0;
        SaveDataScore();
        Debug.Log("Data clear!");
    }
    public static void LoadDataScore()
    {
        string path = Application.persistentDataPath + "/score2.red";
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
      //  if (playerIndex == 0) { return; }

        string path = Application.persistentDataPath + "/score2.red";
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            PlayersData data = new PlayersData(Name,Score,playerIndex);
            formatter.Serialize(stream, data);
            Debug.Log("Save Score");
        }
    }
}
