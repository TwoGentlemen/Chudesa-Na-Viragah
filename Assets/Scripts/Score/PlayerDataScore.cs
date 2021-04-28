using UnityEngine;



[System.Serializable]
public class PlayersData
{
    public string[] name;
    public string[] score;
    public int indexPlayer;

    public PlayersData(string[] _name, string[] _score,int _indexPlayer)
    {
        name = _name;
        score = _score;
        indexPlayer = _indexPlayer;
    }
}