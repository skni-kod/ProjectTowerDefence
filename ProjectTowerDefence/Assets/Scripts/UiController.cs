using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    protected bool upgradeBool;
    protected bool destroyBool;

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
    }

    private void Start()
    {
        upgradeBool = false;
        destroyBool = false;
        GameObject.Find("BuildingsManager").GetComponent<DestroyBuildings>().enabled = false;
        GameObject.Find("BuildingsManager").GetComponent<UpgradingBuildings>().enabled = false;
    }
    public void UpgradeButtonControl()
    {
        if (upgradeBool)
        {
            GameObject.Find("BuildingsManager").GetComponent<UpgradingBuildings>().enabled = false;
            upgradeBool = false;
        }
        else if(!upgradeBool)
        {
            GameObject.Find("BuildingsManager").GetComponent<UpgradingBuildings>().enabled = true;
            if (destroyBool)
            {
                DestroyButtonControl();
            }
            upgradeBool = true;
        }
        print("Upgrade "+ upgradeBool);
        print("Destroy "+ destroyBool);
        print("----------");
    }
    public void DestroyButtonControl()
    {
        if (destroyBool)
        {
            GameObject.Find("BuildingsManager").GetComponent<DestroyBuildings>().enabled = false;
            destroyBool = false;
        }
        else if (!destroyBool)
        {
            GameObject.Find("BuildingsManager").GetComponent<DestroyBuildings>().enabled = true;
            if (upgradeBool)
            {
                UpgradeButtonControl();
            }
            destroyBool = true;
        }
        print("Upgrade " + upgradeBool);
        print("Destroy " + destroyBool);
        print("--------------");
    }

    protected void DisableScript(string objectName, string scriptName)
    {
        GameObject.Find("BuildingsManager").GetComponent<UpgradingBuildings>().enabled = true;
    }
    protected void EnableScript()
    {

    }
}
