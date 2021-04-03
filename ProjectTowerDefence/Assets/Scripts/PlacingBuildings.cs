using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlacingBuildings : MonoBehaviour
{
    public Camera current_camera;       
    public Material green_material;
    public Material red_material;
    public GameObject[] buildings_to_place;
    
    public bool active = false;

    private int building_id = 0;
    private GameObject object_to_place;
    private GameObject green_placeholder;
    private GameObject placed_building;

    #region PublicMethods

    // metoda do uruchamiania skryptu, będzie używana w innych skryptach
    public void PlaceBuilding(GameObject new_object)
    {
        if(!active)
        {
            active = true;
            object_to_place = new_object;
            CreateGreenCopy();
        }            
    }        

    // Dezaktywacja skryptu
    public void Deacivate()
    {
        if(active)
        {
            Destroy(green_placeholder);
            active = false;
        }        
    }
    #endregion

    #region PrivateMethods
    // Utworzenie zielonej kopii obiektu, który będziemy stawiać
    private void CreateGreenCopy()
    {
        // Utworzenie instncji obiektu
        green_placeholder = Instantiate(object_to_place);
        // Ustawienie obiektu na warstwie nr 2, na której obiekty są ignorowane przez Raycast'a
        green_placeholder.layer = 2;

        SetGreenCopyGreenMaterial();
        SetGreenCopyInLayer2();
    }

    // Ustawienie wszystkich podobiektów w obiekcie na zielony kolor
    private void SetGreenCopyGreenMaterial()
    {
        foreach (MeshRenderer child_renderer in green_placeholder.GetComponentsInChildren<MeshRenderer>())
        {
            child_renderer.material = green_material;
        }
    }
    // Ustawienie wszystkich podobiektów na warstwe nr 2
    private void SetGreenCopyInLayer2()
    {
        foreach (Transform child_tranform in green_placeholder.GetComponentsInChildren<Transform>())
        {
            child_tranform.gameObject.layer = 2;
        }
    }
    // Obracanie obiektu o 45 stopni
    private void RotateBuilding()
    {
        green_placeholder.transform.Rotate(Vector3.down, 45);        
    }
    // Zarządzanie stawianiem budynków
    private void HandlePlacing()
    {
        RaycastHit[] hits;
        // Przypisanie promienia, który prowadzony jest z kursora myszki i zwraca wszystkie trafione elementy
        hits = Physics.RaycastAll(current_camera.ScreenPointToRay(Input.mousePosition));

        // Iteracja po każdym trafionym obiekcie
        foreach (RaycastHit hit in hits)
        {   
            // Sprawdza czy trafiony obiekt ma ustawiony tag "Terrain"
            if (hit.collider.CompareTag("Terrain"))
            {
                // Ustawia pozycje obiektu na miejsce trafienia promienia w teren
                green_placeholder.transform.localPosition = hit.point;
                // Jeśli kliknięty LPM to jest zatwierdzane postawienie budynku
                if (Input.GetMouseButtonDown(0))
                {
                    // Tworzenie instancji obiektu w miejscu kursora i z danym obrotem
                    placed_building = Instantiate(object_to_place, hit.point, green_placeholder.transform.rotation);                    
                    Deacivate();
                    // Utworzony obiekt jest ustawiany jako podobiekt "BuildingPlacer"
                    placed_building.transform.SetParent(this.transform);
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
            PlaceBuilding(buildings_to_place[building_id]);
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
                if (building_id == 0)
                {
                    building_id = buildings_to_place.Length - 1;
                }
                else
                {
                    building_id--;
                }
                Deacivate();
                PlaceBuilding(buildings_to_place[building_id]);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (building_id == buildings_to_place.Length - 1)
                {
                    building_id = 0;
                }
                else
                {
                    building_id++;
                }
                Deacivate();
                PlaceBuilding(buildings_to_place[building_id]);
            }

            HandlePlacing();
        }
    }
}
