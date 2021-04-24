using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [Header("Player Data")]
    [SerializeField] private PlayerDataSO playerData;

    [Space(5)]
    [Header("User interface")]
    [SerializeField] private Slider sliderFuel;
    [SerializeField] private Text textTimer;
    [SerializeField] private Text textCoin;

    [Space(5)]
    [Header("Panels menu")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelOwer;




    private Car _car;
    private GameObject carPos;
    private float currentVolumeFuel = 0;
    private int tankFuel = 0;
    private int fuelConsuption = 1; //Расход топлива л/сек
    private float frequencyFuel = 0;

    private bool isPause = false;

    private void Awake()
    {
        if(instance != null) { Debug.LogError("instance in GameManager");}
        instance = this;

       // Load();

        if(playerData.cars != null && playerData != null)
        {
            if(playerData.cars.Length < playerData.currentCar) { playerData.currentCar = 0;}

            carPos = Instantiate(playerData.cars[playerData.currentCar].carModels,transform.position,Quaternion.identity);
            _car = carPos.GetComponent<Car>();
        }

        Time.timeScale = 1;
    }
    private void Start()
    {
        if(_car == null) { Debug.LogError("_car == null");}
        fuelConsuption = _car.fuelConsuption;
        tankFuel = _car.tankFuel;
        currentVolumeFuel = tankFuel;

        StartCoroutine(FuelConsuption());
        textCoin.text = playerData.coins+"";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            if (isPause)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

    }
    IEnumerator FuelConsuption()
    {
        frequencyFuel = (float)fuelConsuption/10;

        while (currentVolumeFuel > 0)
        {
            currentVolumeFuel-=frequencyFuel;
            sliderFuel.value = currentVolumeFuel/(float)tankFuel;
            yield return new WaitForSeconds(0.1f);
        }

        if(currentVolumeFuel <= 0)
        {
            GameOwer();
        }
    }

    private void Save()
    {
        SaveSystem.SavePlayer(playerData);
    }

    private void Load()
    {
        SaveData data = SaveSystem.LoadData();
        if(data == null) { return;}

        playerData.currentCar = data.currentCar;
        playerData.coins = data.coins;

        
    }
    public void NextLevel()
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        if(SceneManager.sceneCountInBuildSettings <= playerData.levelCompleted)
        {
            GameRestart();
        }
        else
        {
            LoadLevel(playerData.levelCompleted);
        }
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void GameWin()
    {
        var buf = SceneManager.GetActiveScene().buildIndex+1;
        if (playerData.levelCompleted <= buf)
        {
            playerData.levelCompleted = buf;
        }
        panelWin.SetActive(true);

        SaveSystem.SavePlayer(playerData);
        
    }

    private void GameOwer()
    {
        _car.StopCar();
        panelOwer.SetActive(true);
    }

    public void ResumeGame()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1;
        isPause = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AddFuel(int volume)
    {
        currentVolumeFuel+=volume;
        currentVolumeFuel = Mathf.Min(currentVolumeFuel,tankFuel);
    }

    public void AddCoin(int count)
    {
        playerData.coins+=count;
        textCoin.text = playerData.coins+"";
    }
}
