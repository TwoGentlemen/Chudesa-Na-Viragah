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
    private int fuelConsuption = 1; //Расход топлива л/сек
    private float frequencyFuel = 0;

    private bool isPause = false;
    private bool isGame = true; //Идет ли игра

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
        isGame = true;
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
        frequencyFuel = (float)fuelConsuption/10;

        while (currentVolumeFuel > 0 && isGame)
        {
            currentVolumeFuel-=frequencyFuel;
            sliderFuel.value = currentVolumeFuel/(float)tankFuel;
            yield return new WaitForSeconds(0.1f);
        }

        if(currentVolumeFuel <= 0 )
        {
            GameOwer();
        }
    }

    private void SetScorePanel()
    {
        var a = SceneManager.GetActiveScene().buildIndex - 1;
        if (a < 0 || a >= SaveScore.countLevel) { Debug.LogError("Error"); return; }

        panelSaveScore.SetActive(true);
        textScore.text = "Your score: "+timer.GetTime();
        inputName.text = (SaveScore.playerIndex[a] == 0)? "Player" + Random.Range(1, 999):SaveScore.Name[a,SaveScore.playerIndex[a]-1];

    }

    public void buttonClickSaveScore()
    {
        if(inputName.text == "") { inputName.text = "Player"+ Random.Range(1,999);}

        var a = SceneManager.GetActiveScene().buildIndex - 1;
        if (a<0 || a >= SaveScore.countLevel) { Debug.LogError("Error"); return;}
        Debug.Log(a);
        SaveScore.AddPlayer(inputName.text, timer.GetTime(),a);

        panelSaveScore.SetActive(false);
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
        SaveSystem.SaveData(playerData);

        if (!isGame) { return; }
        
        SetScorePanel();
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
