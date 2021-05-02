using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradingBuildings : MonoBehaviour
{
    public Camera currentCamera;

    private GameObject upgradeUI;

    private GraphicRaycaster graphicRaycaster;
    private EventSystem eventSystem;
    private PointerEventData pointerEventData;

    private GameObject clickedBuilding;

    private Text buildingName;

    public void UpgradeBuilding()
    {
        // TO DO
    }    

    bool CheckObjectHit(string tag)
    {
        RaycastHit[] hits;
        // Przypisanie promienia, który prowadzony jest z kursora myszki i zwraca wszystkie trafione elementy
        hits = Physics.RaycastAll(currentCamera.ScreenPointToRay(Input.mousePosition));

        // Iteracja po każdym trafionym obiekcie
        foreach (RaycastHit hit in hits)
        {            
            // Sprawdza czy trafiony obiekt ma ustawiony tag "tag"
            if (hit.transform.gameObject.CompareTag(tag))
            {
                clickedBuilding = hit.transform.gameObject;
                return true;
            }
        }
        return false;
    }

    bool CheckUIHit()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        graphicRaycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            return true;
        }
        return false;
    }

    void UpgradeUISetActive(bool check)
    {
        upgradeUI.SetActive(check);        
        if(check)
        {
            buildingName.text = clickedBuilding.name;
            upgradeUI.transform.position = Input.mousePosition;
        }        
    }
    
    void Start()
    {
        graphicRaycaster = GetComponentInChildren<GraphicRaycaster>(true);
        eventSystem = GetComponent<EventSystem>();

        buildingName = transform.Find("UpgradeUI/Canvas/Background/BuildingName").gameObject.GetComponent<Text>();

        upgradeUI = transform.Find("UpgradeUI").gameObject;
        if(upgradeUI == null)
        {
            Debug.LogError("upgradeUI not found!");
        }        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {            
            if(!upgradeUI.activeSelf)
            {                
                UpgradeUISetActive(CheckObjectHit("Building"));
            }
            else
            {
                UpgradeUISetActive(CheckUIHit());
            }
        }        
    }    
}
