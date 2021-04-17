using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellsCasting : MonoBehaviour
{
    public Camera current_camera;              
    public GameObject cylinder;
    
    public bool active = false;

    private GameObject spell;
    private GameObject cylinder_instance;
    private GameObject cast_area;

    #region PublicMethods

    public void CastSpell(GameObject input_spell)
    {
        if(!active)
        {
            active = true;
            spell = input_spell;
            CreateInstance();
        }            
    }        

    // Dezaktywacja skryptu
    public void Deacivate()
    {
        if(active)
        {
            Destroy(cylinder_instance);
            active = false;
        }        
    }
    #endregion

    #region PrivateMethods
    // Utworzenie zielonej kopii obiektu, który będziemy stawiać
    private void CreateInstance()
    {
        // Utworzenie instncji obiektu
        cylinder_instance = Instantiate(cylinder);
        // Ustawienie obiektu na warstwie nr 2, na której obiekty są ignorowane przez Raycast'a
        cylinder_instance.layer = 2;

        SetInstanceInLayer2(cylinder_instance);
    }
    
    // Ustawienie wszystkich podobiektów na warstwe nr 2
    private void SetInstanceInLayer2(GameObject instance)
    {
        foreach (Transform child_tranform in instance.GetComponentsInChildren<Transform>())
        {
            child_tranform.gameObject.layer = 2;
        }
    }
    
    // Zarządzanie rzucaniem zaklęć
    private void HandleCasting()
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
                cylinder_instance.transform.localPosition = hit.point;
                // Jeśli kliknięty LPM to jest zatwierdzane rzucenie zaklęcia
                if (Input.GetMouseButtonDown(0))
                {                 
                    // Tworzenie instancji obiektu w miejscu kursora
                    cast_area = Instantiate(cylinder, hit.point, cylinder_instance.transform.rotation);

                    Vector3 new_scale = Vector3.Scale(cast_area.transform.localScale, new Vector3(1, 1, 0.3f));

                    cast_area.transform.localScale = new_scale;

                    // Utworzony obiekt jest ustawiany jako podobiekt "SpellsCaster"
                    cast_area.transform.SetParent(this.transform);

                    Deacivate();
                }                
            }
        }
    }    
    #endregion

    void Update()
    {
        //Uruchamianie skryptu
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CastSpell(spell);
        }                       

        if (active)
        {
            // Wyłączanie skryptu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Deacivate();
            }

            HandleCasting();
        }
    }
}
