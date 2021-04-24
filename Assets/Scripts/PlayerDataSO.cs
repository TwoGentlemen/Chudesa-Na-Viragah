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
}
