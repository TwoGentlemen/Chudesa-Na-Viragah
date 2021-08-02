using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [Header("Player Data")]
    [SerializeField] private PlayerDataSO playerData;

    [Space(5)]
    [Header("User interface")]
    [SerializeField] private Image sliderFuel;
    [SerializeField] private Text quantityFuel;
    [SerializeField] private Text textCoin;

    [Space(5)]
    [Header("Panels menu")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelOwer;

    [Space(5)]
    [SerializeField] private GameObject panelSaveScore;
    [SerializeField] private ScoreTime timer;
    [SerializeField] private Text textScore;
    [SerializeField] private InputField inputName;




    private Car _car;
    private GameObject carPos;
    private float currentVolumeFuel = 0;
    private int tankFuel = 0;
    private float frequencyFuel = 0;

    private bool isPause = false;
    private bool isGame = true; //»дет ли игра

    private void Awake()
    {
        if(instance != null) { Debug.LogError("instance in GameManager");}
        instance = this;
    }
    private void Start()
    {
        if (playerData.cars != null && playerData != null)
        {
            if (playerData.cars.Length < playerData.currentCar) { playerData.currentCar = 0; }

            carPos = Instantiate(playerData.cars[playerData.currentCar].carModels, transform.position, Quaternion.identity);
            _car = carPos.GetComponent<Car>();
            _car.enabled = true;
            _car.SetTargetPosition(0.5f);

            if(CameraMove.instance != null)
                CameraMove.instance.SetTarget(carPos.transform);
        }

        Time.timeScale = 1;
        isGame = true;

        if (_car == null) { Debug.LogError("_car == null");}
        tankFuel = _car.tankFuel;
        currentVolumeFuel = tankFuel;

        StartCoroutine(FuelConsuption());
        textCoin.text = playerData.coins+"";
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))&& isGame)
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
        frequencyFuel = 1f/10f;
        int startQuantityFuel = (int)currentVolumeFuel;


        while (currentVolumeFuel > 0 && isGame)
        {
            currentVolumeFuel-=frequencyFuel;
            sliderFuel.fillAmount = currentVolumeFuel/(float)tankFuel;
            quantityFuel.text = (int)currentVolumeFuel + "/"+startQuantityFuel;
            yield return new WaitForSeconds(0.1f);
        }

        if(currentVolumeFuel <= 0 )
        {
            GameOwer();
        }
    }

    private int _gaz = 0;
    public int Gaz()
    {
        return _gaz;
    }
    public void AddGaz(int i)
    {
        _gaz=i;
    }


    private void SetScorePanel()
    {
        var indexCurrentScene = SceneManager.GetActiveScene().buildIndex;
        
    }



    public void GameWin()
    {
        SaveSystem.SaveData(playerData);

        if (!isGame) { return; }

        isGame = false;
        var buf = SceneManager.GetActiveScene().buildIndex+1;
        if (playerData.levelCompleted <= buf)
        {
            playerData.levelCompleted = buf;
        }
        panelWin.SetActive(true);
        _car.StopCar();
        
    }

    private void GameOwer()
    {
       // YandexSDK.instance.ShowInterstitial();
        SaveSystem.SaveData(playerData);

        if (!isGame) { return;}
        isGame = false;
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
        SaveSystem.SaveData(playerData);

        if (!isGame) { return; }
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }
    public void GameRestart()
    {
        SaveSystem.SaveData(playerData);

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
