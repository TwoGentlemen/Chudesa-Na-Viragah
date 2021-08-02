using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Transform spawnCar;
    [SerializeField] private PlayerDataSO playerData;

    [SerializeField] private Text buttonText;
    [SerializeField] private Button buttonBuy;
    [SerializeField] private Text countCoin;
    

    [Space(5)]
  //  [SerializeField] private GameObject panelInfoCar;
    //[SerializeField] private Text textNameCar;
    [SerializeField] private Text textPriceCar;
   // [SerializeField] private Text textMaxSpeedCar;
   // [SerializeField] private Text textDescriptionCar;

    private GameObject[] carModel;
    private int indexCar = 0;

    private YandexSDK sdk;

    private void Start()
    {
        Time.timeScale = 1;

        if (playerData.cars.Length <= 0) { Debug.LogError("Not cars"); return; }
        carModel = new GameObject[playerData.cars.Length];

        for (int i = 0; i < playerData.cars.Length; i++)
        {
            carModel[i] = Instantiate(playerData.cars[i].carModels, spawnCar.position, spawnCar.rotation);

            if (i != playerData.currentCar)
            {
                carModel[i].SetActive(false);
            }
            else
            {
                indexCar = i;
            }

            carModel[i].GetComponent<Car>().enabled = false;
        }

        sdk = YandexSDK.instance;
        sdk.onRewardedAdReward+=FreeCoins;

        SelectedCar();
        ChangeCountCoin();
    }
    private void ChangeCountCoin()
    {
        countCoin.text = playerData.coins+"";
    }

    private void SelectedCar()
    {
        
        if (playerData.cars[indexCar].isBuy)
        {
            if(indexCar != playerData.currentCar)
            {
                buttonText.text = "Выбрать";
                buttonBuy.interactable = true;
            }
            else
            {
                buttonText.text = "Выбран";
                buttonBuy.interactable = false;
            }
            

            SetPricaCar(false);
        }
        else
        {
            

            if (playerData.coins < playerData.cars[indexCar].price)
            {
                buttonText.text = "Нет монет";
                buttonBuy.interactable = false;
            }
            else
            {
                
                buttonText.text = "Купить";
                buttonBuy.interactable = true;
            }

            SetPricaCar(true);
        }
    }

    public void NextCar()
    {
        carModel[indexCar].SetActive(false);
        indexCar = (indexCar+1) % carModel.Length;
        carModel[indexCar].SetActive(true);

        SelectedCar();
        

    }

    public void BackCar()
    {
        carModel[indexCar].SetActive(false);
        if(indexCar == 0)
        {
            indexCar=carModel.Length-1;
        }
        else { 
        indexCar = Mathf.Abs((indexCar - 1)) % carModel.Length; }
        carModel[indexCar].SetActive(true);

        SelectedCar();
        
    }

    public void ButtonBuy()
    {
        if (playerData.cars[indexCar].isBuy)
        {
            buttonText.text = "Выбран";
            buttonBuy.interactable = false;

            playerData.currentCar = indexCar;

            SaveSystem.SaveData(playerData);
            return;
        }

        playerData.cars[indexCar].isBuy = true;
        playerData.coins-=playerData.cars[indexCar].price;

        SaveSystem.SaveData(playerData);

        SelectedCar();
        ChangeCountCoin();
    }

    public void SetPricaCar(bool isActive)
    {
        if(isActive)
            textPriceCar.text = playerData.cars[indexCar].price+"$";    
        else
            textPriceCar.text = "";
    }

    public void FreeCoins(string paramentr)
    {
        playerData.coins+=200;
        ChangeCountCoin();
    }
}
