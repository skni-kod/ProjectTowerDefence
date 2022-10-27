using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    protected float gameSpeed = 1.0f;
    
    protected bool upgradeBool;
    protected bool destroyBool;

    protected Color32 redColor;
    protected Color32 greenColor;

    protected UpgradingBuildings upgradingBuildings;
    protected DestroyBuildings destroyBuildings;
    protected PlacingBuildings placingBuildings;

    public GameObject upgradeButton;
    public GameObject destroyButton;

    protected Image upgradeButtonImage;
    protected Image destroyButtonImage;

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void SetGameSpeed(float speed)
    {
        gameSpeed = speed;
        Time.timeScale = speed;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = gameSpeed;
    }

    private void Start()
    {
        OnStartGet();

        upgradeBool = false;
        destroyBool = false;

        upgradingBuildings.enabled = false;
        destroyBuildings.enabled = false;

        upgradeButtonImage.color = redColor;
        destroyButtonImage.color = redColor;

    }

    public void UpgradeButtonControl()
    {
        if (upgradeBool)
        {
            upgradingBuildings.enabled = false;
            upgradeButtonImage.color = redColor;
            upgradeBool = false;
        }
        else if(!upgradeBool)
        {
            upgradingBuildings.enabled = true;
            upgradeButtonImage.color = greenColor;
            if (destroyBool)
            {
                DestroyButtonControl();
            }
            upgradeBool = true;
        }
    }

    public void DestroyButtonControl()
    {
        if (destroyBool)
        {
            destroyBuildings.enabled = false;
            destroyButtonImage.color = redColor;
            destroyBool = false;
        }
        else if (!destroyBool)
        {
            destroyBuildings.enabled = true;
            destroyButtonImage.color = greenColor;
            if (upgradeBool)
            {
                UpgradeButtonControl();
            }
            destroyBool = true;
        }
    }

    protected void OnStartGet()
    {
        upgradingBuildings = GameObject.Find("BuildingsManager").GetComponent<UpgradingBuildings>();
        destroyBuildings = GameObject.Find("BuildingsManager").GetComponent<DestroyBuildings>();
        placingBuildings = GameObject.Find("BuildingsManager").GetComponent<PlacingBuildings>();

        upgradeButtonImage = upgradeButton.GetComponent<Image>();
        destroyButtonImage = destroyButton.GetComponent<Image>();

        redColor = new Color32(128, 0, 0, 255);
        greenColor = new Color32(0, 128, 0, 255);
    }

    public void SetTowerId(int towerId)
    {
        placingBuildings.buildingId = towerId;
    }
}
