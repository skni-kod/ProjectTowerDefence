using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

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

    [SerializeField] protected GameObject upgradeButton;
    [SerializeField] protected GameObject destroyButton;

    protected Image upgradeButtonImage;
    protected Image destroyButtonImage;

    [SerializeField] protected ResourcesController ownerResources; 
    [SerializeField] protected Text Gold;
    [SerializeField] protected Text Wood;
    [SerializeField] protected Text Supply;
    [SerializeField] protected Text Mana;

    protected List<int> CurrentResources = new List<int>();
    protected int MaxSupply;
    

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

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentSceneIndex+1);
        }
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

        for(int i = 0; i < (int)Resource.ResourcesTypes.End; i++)
        {
            CurrentResources.Add(0);
        }

    }

    private void Update()
    {
        /*if(levelController.GetCurrentPhase == LevelController.LevelPhase.LEVEL_COMPLETED)
        {
            
        }*/
        /*
        Jeżeli to czytasz to znaczy że stwierdziłeś że linie kodu poniżej są głupie. Problem jest taki, ze nie są 
        Wartości będą zmieniane bardzo często więc nie ma sensu wrzucać tego w funkcje i zmieniać co zmianę wartości pojedyńczego pola
        Dlatego łatwiej i lepiej jest zrobić to tym sposobem 
        Jezeli coś zmienisz, to prosze zrób to tak, żeby to działało 
        Powodzenia
        */
        UpdateResources();
        Gold.text = "Gold:" + CurrentResources[(int)Resource.ResourcesTypes.Gold].ToString();
        Wood.text = "Wood:" + CurrentResources[(int)Resource.ResourcesTypes.Wood].ToString();
        Supply.text = "Supply:" + CurrentResources[(int)Resource.ResourcesTypes.Supply].ToString() + "/" + MaxSupply.ToString();
        Mana.text = "Mana:" + CurrentResources[(int)Resource.ResourcesTypes.Mana].ToString();
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
    private void UpdateResources()
    {
        List<Resource> resources = ownerResources.GetResourcesRef();
        for(int i = 0; i < (int)Resource.ResourcesTypes.End; i++)
        {
            CurrentResources[i] = (int)resources[i].amount;
        }
        MaxSupply= (int)resources[(int)Resource.ResourcesTypes.Supply].MaxAmmount;

    }

}
