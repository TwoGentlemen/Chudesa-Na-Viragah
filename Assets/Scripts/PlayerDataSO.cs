using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tools",menuName = "Data")]
public class PlayerDataSO : ScriptableObject
{
    public int levelCompleted = 0;
    public int currentCar = 0;
    public int coins = 0;

    public Cars[] cars;

    public void Load(Data data)
    {
        if(data == null) {Debug.Log("null data"); return; }
        levelCompleted = data.levelCompleted;
        currentCar = data.currentCar;
        coins = data.coins;
        
        if(cars == null || cars.Length != data.isBuyCar.Length) { Debug.LogError("No cars!"); return;}
        for (int i = 0; i < data.isBuyCar.Length; i++)
        {
            cars[i].isBuy = data.isBuyCar[i];
        }
    } 

}
[System.Serializable]
public class Data
{
    public int levelCompleted = 0;
    public int currentCar = 0;
    public int coins = 0;

    public bool[] isBuyCar;
    public Data(PlayerDataSO playerData)
    {
        levelCompleted = playerData.levelCompleted;
        currentCar = playerData.currentCar;
        coins = playerData.coins;
       
        if(playerData.cars == null) { Debug.LogError("No cars!"); return; }
        isBuyCar = new bool[playerData.cars.Length];

        for (int i = 0; i < isBuyCar.Length; i++)
        {
            isBuyCar[i] = playerData.cars[i].isBuy;
        }
    }
}
