using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlacingBuildings : MonoBehaviour
{
    public Camera currentCamera;       
    public Material greenMaterial;
    public Material redMaterial;
    public GameObject[] buildingsToPlace;
    
    public bool active = false;

    public int buildingId = 0;
    private GameObject objectToPlace;
    private GameObject greenPlaceholder;
    private GameObject placedBuilding;
    private InputMaster controls;
    public bool isBuild;
    #region PublicMethods
    
    // metoda do uruchamiania skryptu, będzie używana w innych skryptach
    public void OnBuildstart()
    {
        if(!active)
        {
            active = true;
            objectToPlace = buildingsToPlace[buildingId];
            CreateGreenCopy();
        }            
    }        

    // Dezaktywacja skryptu
    public void OnBuildcancel()
    {
        if(active)
        {
            Destroy(greenPlaceholder);
            active = false;
        }        
    }
    #endregion

    #region PrivateMethods
    // ustawienie tagu wszystkich podobiektów
    void SetAllChildrenTag(GameObject gameObject, string tag)
    {
        Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in allChildren)
        {
            child.gameObject.tag = tag;
        }
    }

    // Utworzenie zielonej kopii obiektu, który będziemy stawiać
    private void CreateGreenCopy()
    {
        // Utworzenie instncji obiektu
        greenPlaceholder = Instantiate(objectToPlace);
        // Ustawienie obiektu na warstwie nr 2, na której obiekty są ignorowane przez Raycast'a
        greenPlaceholder.layer = 2;
        //Wyłączenie skryptu odpowiadającego za atakowanie oraz paska cooldownu
        greenPlaceholder.GetComponent<Tower>().enabled = false;
        greenPlaceholder.transform.Find("CooldownBar").gameObject.SetActive(false);

        SetGreenCopyGreenMaterial(false);
        SetGreenCopyInLayer2();
    }

    // Ustawienie wszystkich podobiektów w obiekcie na zielony kolor
    private void SetGreenCopyGreenMaterial(bool red)
    {
        foreach (MeshRenderer childRenderer in greenPlaceholder.GetComponentsInChildren<MeshRenderer>())
        {
            childRenderer.material = !red?greenMaterial:redMaterial;
        }
    }
    // Ustawienie wszystkich podobiektów na warstwe nr 2
    private void SetGreenCopyInLayer2()
    {
        foreach (Transform childTranform in greenPlaceholder.GetComponentsInChildren<Transform>())
        {
            childTranform.gameObject.layer = 2;
        }
    }
    // Obracanie obiektu o 45 stopni
    private void RotateBuilding()
    {
        greenPlaceholder.transform.Rotate(Vector3.down, 45);        
    }
    // Zarządzanie stawianiem budynków
    private void OnBuildnew()
    {
        if (!active) { return; }
        RaycastHit[] hits;
        // Przypisanie promienia, który prowadzony jest z kursora myszki i zwraca wszystkie trafione elementy
        hits = Physics.RaycastAll(currentCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        bool isEmpty = true;
        Vector3 poz=Vector3.zero;
        // Iteracja po każdym trafionym obiekcie
        foreach (RaycastHit hit in hits)
        {   
            
            // Sprawdza czy trafiony obiekt ma ustawiony tag "Terrain"
            if (hit.collider.CompareTag("Terrain"))
            {
                poz = hit.point;
            }
            else if (hit.collider.CompareTag("Building") || hit.collider.CompareTag("Towers"))
            {
                isEmpty = false;
                //   SetGreenCopyGreenMaterial(true);
            }
            else if (hit.collider.CompareTag("NonPlaceableTerrain"))
            {
                isEmpty = false;
            }


        }

        if (isEmpty)
        {
            // Ustawia pozycje obiektu na miejsce trafienia promienia w teren
           
            // Jeśli kliknięty LPM to jest zatwierdzane postawienie budynku

            // Tworzenie instancji obiektu w miejscu kursora i z danym obrotem

            placedBuilding = Instantiate(objectToPlace, poz, greenPlaceholder.transform.rotation);
            placedBuilding.name = "Tower " + transform.childCount.ToString();

            SetAllChildrenTag(placedBuilding, "Towers");

            OnBuildcancel();
            // Utworzony obiekt jest ustawiany jako podobiekt "BuildingPlacer"
            placedBuilding.transform.SetParent(this.transform);

            // if (Input.GetMouseButtonDown(1))               
            //     RotateBuilding();
        }
        else
        { return; }
    }
    private void setPositionBuilding()
    {
        if (!active) { return; }
        RaycastHit[] hits;
        // Przypisanie promienia, który prowadzony jest z kursora myszki i zwraca wszystkie trafione elementy
        hits = Physics.RaycastAll(currentCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        bool isEmpty=true ;
        // Iteracja po każdym trafionym obiekcie
        foreach (RaycastHit hit in hits)
        {
            // Sprawdza czy trafiony obiekt ma ustawiony tag "Terrain"
            if (hit.collider.CompareTag("Terrain"))
            {
                // Ustawia pozycje obiektu na miejsce trafienia promienia w teren
                greenPlaceholder.transform.localPosition = hit.point;
                // Jeśli kliknięty LPM to jest zatwierdzane postawienie budynku

                // Tworzenie instancji obiektu w miejscu kursora i z danym obrotem

                // Utworzony obiekt jest ustawiany jako podobiekt "BuildingPlacer"
                //  placedBuilding.transform.SetParent(this.transform);

                // if (Input.GetMouseButtonDown(1))               
                //     RotateBuilding();


            }
            else if(hit.collider.CompareTag("Building") || hit.collider.CompareTag("Towers"))
            {
                isEmpty = false;
            }
            else if (hit.collider.CompareTag("NonPlaceableTerrain"))
            {
                greenPlaceholder.transform.localPosition = hit.point;
                isEmpty = false;
            }
        }
        SetGreenCopyGreenMaterial(!isEmpty);
    }
    //Zarządzanie rodzajem stawianego budynku
    private void OnBuildchangeId(InputValue val)
    {
      bool plus = val.Get<float>() > 0;
        if (!active) { return; }
        if(plus)
        {
            if (buildingId == 0)
            {
                buildingId = buildingsToPlace.Length - 1;
            }
            else
            {
                buildingId--;
            }
            OnBuildcancel();
            OnBuildstart();
        }
        else
        {
            if (buildingId == buildingsToPlace.Length - 1)
            {
                buildingId = 0;
            }
            else
            {
                buildingId++;
            }
            OnBuildcancel();
            OnBuildstart();
        }
    }
    #endregion

    private void Start()
    {
        currentCamera =FindObjectOfType<Camera>();
        active = false;
    }
    void Update()
    {
       if (active)
        {

            isBuild = true;
            setPositionBuilding();
        }
       else
        {
            Invoke("buildOff", 0.1f);
        }
    }
    private void buildOff()
    {
        isBuild = false;
    }

}
