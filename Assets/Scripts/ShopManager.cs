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
    [SerializeField] private Text textPrice;
    [SerializeField] private Text countCoin;
    [SerializeField] private Text nameCar;

    private GameObject[] carModel;
    private int indexCar = 0;
    private void Awake()
    {
        Time.timeScale = 1;

        if(playerData.cars.Length <= 0) { Debug.LogError("Not cars"); return;}
        carModel = new GameObject[playerData.cars.Length];

        for (int i = 0; i < playerData.cars.Length; i++)
        {
            carModel[i] = Instantiate(playerData.cars[i].carModels,spawnCar.position,spawnCar.rotation);

            if (i != playerData.currentCar) { 
            carModel[i].SetActive(false);
            }
            else
            {
                indexCar=i;
            }
        }

    }

    private void Start()
    {
        SelectedCar();
        ChangeCountCoin();
    }
    private void ChangeCountCoin()
    {
        countCoin.text = playerData.coins+"";
    }

    private void SelectedCar()
    {
        nameCar.text = playerData.cars[indexCar].name;
        if (playerData.cars[indexCar].isBuy)
        {
            textPrice.text = "";
            buttonText.text = "Selected";
            buttonBuy.interactable = false;

            playerData.currentCar = indexCar;
        }
        else
        {
            textPrice.text = playerData.cars[indexCar].price + " coins";

            if (playerData.coins < playerData.cars[indexCar].price)
            {
                buttonText.text = "No coins";
                buttonBuy.interactable = false;
            }
            else
            {
                
                buttonText.text = "Buy";
                buttonBuy.interactable = true;
            }

           
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
        playerData.cars[indexCar].isBuy = true;
        playerData.coins-=playerData.cars[indexCar].price;

        SaveSystem.SaveData(playerData);

        SelectedCar();
        ChangeCountCoin();
    }

}
