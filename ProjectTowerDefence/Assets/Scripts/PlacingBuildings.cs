using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlacingBuildings : MonoBehaviour
{
    public Camera currentCamera;       
    public Material greenMaterial;
    public Material redMaterial;
    public GameObject[] buildingsToPlace;
    
    public bool active = false;

    private int buildingId = 0;
    private GameObject objectToPlace;
    private GameObject greenPlaceholder;
    private GameObject placedBuilding;

    #region PublicMethods

    // metoda do uruchamiania skryptu, będzie używana w innych skryptach
    public void PlaceBuilding(GameObject new_object)
    {
        if(!active)
        {
            active = true;
            objectToPlace = new_object;
            CreateGreenCopy();
        }            
    }        

    // Dezaktywacja skryptu
    public void Deacivate()
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

        SetGreenCopyGreenMaterial();
        SetGreenCopyInLayer2();
    }

    // Ustawienie wszystkich podobiektów w obiekcie na zielony kolor
    private void SetGreenCopyGreenMaterial()
    {
        foreach (MeshRenderer childRenderer in greenPlaceholder.GetComponentsInChildren<MeshRenderer>())
        {
            childRenderer.material = greenMaterial;
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
    private void HandlePlacing()
    {
        RaycastHit[] hits;
        // Przypisanie promienia, który prowadzony jest z kursora myszki i zwraca wszystkie trafione elementy
        hits = Physics.RaycastAll(currentCamera.ScreenPointToRay(Input.mousePosition));

        // Iteracja po każdym trafionym obiekcie
        foreach (RaycastHit hit in hits)
        {   
            // Sprawdza czy trafiony obiekt ma ustawiony tag "Terrain"
            if (hit.collider.CompareTag("Terrain"))
            {
                // Ustawia pozycje obiektu na miejsce trafienia promienia w teren
                greenPlaceholder.transform.localPosition = hit.point;
                // Jeśli kliknięty LPM to jest zatwierdzane postawienie budynku
                if (Input.GetMouseButtonDown(0))
                {
                    // Tworzenie instancji obiektu w miejscu kursora i z danym obrotem
                    placedBuilding = Instantiate(objectToPlace, hit.point, greenPlaceholder.transform.rotation);

                    SetAllChildrenTag(placedBuilding, "Building");

                    Deacivate();
                    // Utworzony obiekt jest ustawiany jako podobiekt "BuildingPlacer"
                    placedBuilding.transform.SetParent(this.transform);
                }
                if (Input.GetMouseButtonDown(1))               
                    RotateBuilding();
            }
        }
    }    
    #endregion

    void Update()
    {
        //Uruchamianie skryptu
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceBuilding(buildingsToPlace[buildingId]);
        }                       

        if (active)
        {
            // Wyłączanie skryptu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Deacivate();
            }

            // Zmiana aktualnie stawianego obiektu
            // Funkcja tymczasowa, aby urozmaicić działanie skryptu
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (buildingId == 0)
                {
                    buildingId = buildingsToPlace.Length - 1;
                }
                else
                {
                    buildingId--;
                }
                Deacivate();
                PlaceBuilding(buildingsToPlace[buildingId]);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (buildingId == buildingsToPlace.Length - 1)
                {
                    buildingId = 0;
                }
                else
                {
                    buildingId++;
                }
                Deacivate();
                PlaceBuilding(buildingsToPlace[buildingId]);
            }

            HandlePlacing();
        }
    }
}
