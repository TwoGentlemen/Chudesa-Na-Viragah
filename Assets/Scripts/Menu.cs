using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Animator animatorCamera;
    public GameObject panelShop;
    public GameObject panelLevels;
    public GameObject panelMainMenu;

    [Space(5)]
    [SerializeField] private PlayerDataSO playerData;
    [SerializeField] private Button[] levelButton;

    private void UnlockLevels()
    {
        if(playerData.levelCompleted > levelButton.Length)
        {
            playerData.levelCompleted = levelButton.Length-1;
        }

        for (int i = 0; i < levelButton.Length; i++)
        {
            if (i < playerData.levelCompleted)
            {
                levelButton[i].interactable = true;
                levelButton[i].image.color = new Color(255,255,255);
            }
            else
            {
                levelButton[i].interactable = false;
                levelButton[i].image.color = new Color(255, 255, 255,0.4f);
            }
        }
    }

    private void Awake()
    {
        Load();

        UnlockLevels();
    }

    private void Load()
    {
        Data data = SaveSystem.LoadData();
        playerData.Load(data);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void OpenLevels()
    {
        panelMainMenu.SetActive(false);
        panelLevels.SetActive(true);
    }

    public void CloseLevels()
    {
        panelLevels.SetActive(false);
        panelMainMenu.SetActive(true);
    }

    public void OpenShopAnimation()
    {
        panelShop.SetActive(true);
    }
    public void OpenShop()
    {
        panelMainMenu.SetActive(false);
        animatorCamera.SetTrigger("ShopOpen");
    }

    public void CloseShopAnimation()
    {
        panelMainMenu.SetActive(true);
    }
    public void CloseShop()
    {
        panelShop.SetActive(false);
        animatorCamera.SetTrigger("ShopClose");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
