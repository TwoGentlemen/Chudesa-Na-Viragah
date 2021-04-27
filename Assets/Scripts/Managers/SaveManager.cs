using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private PlayerDataSO playerData;

 
    private void Awake()
    {
        Data data = SaveSystem.LoadData();
        playerData.Load(data);

        DontDestroyOnLoad(gameObject);             
    }

    

    private void OnApplicationQuit()
    {
        SaveSystem.SaveData(playerData);
    }

}
