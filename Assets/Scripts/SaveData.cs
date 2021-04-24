

[System.Serializable]
public class SaveData
{
    public int levelCompleted = 0;
    public int coins = 0;
    public int currentCar = 0;

    public SaveData(PlayerDataSO player)
    {
        levelCompleted = player.levelCompleted;
        coins = player.coins;
        currentCar = player.currentCar;
    }


}
