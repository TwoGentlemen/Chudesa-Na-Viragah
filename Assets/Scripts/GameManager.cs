using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [Header("User interface")]
    [SerializeField] private Slider sliderFuel;
    [SerializeField] private Text textTimer;
    [SerializeField] private Text textCoin;

    [Space(5)]
    [Header("Panels menu")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelOwer;

    [Space(5)]
    [Header("Cars objects")]
    [SerializeField] private GameObject carObject;//Временно


    private float _timer = 0;
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

        if(carObject != null)
        {
            carPos = Instantiate(carObject,transform.position,Quaternion.identity);
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
}
