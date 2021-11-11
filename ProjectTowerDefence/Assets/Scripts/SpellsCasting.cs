using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellsCasting : MonoBehaviour
{
    public Camera currentCamera;
    public GameObject cylinder;

    //w celu debugowania jest publiczne
    public bool setArea = false;
    public bool preparedSpell = false;
    public bool workingSpell = false;

    private SpellBook spell;
    private GameObject cylinderInstance;
    private GameObject castArea;

    private List<SpellBook> spellList = new List<SpellBook>( new SpellBook[] { new TestSpell(), new IceSpell(), new IceSpell() });

    private IceSpell ice;

    #region PublicMethods       

    public void ActivePrepareCastingSpell(int index)
    {
        if(!setArea)
        {
            setArea = true;
            spell = spellList[index];
            CreateInstance();
        }            
    }        

    // Dezaktywacja skryptu
    public void Deacivate()
    {
        if(setArea)
        {
            Destroy(cylinderInstance);
            setArea = false;
        }        
    }
    #endregion

    #region PrivateMethods
    // Utworzenie zielonej kopii obiektu, który będziemy stawiać
    private void CreateInstance()
    {
        // Utworzenie instncji obiektu
        cylinderInstance = Instantiate(cylinder);

        // ustawienie rozmiaru obszaru na podstawie danych z klasy zaklęcia
        Vector3 new_scale = Vector3.Scale(cylinderInstance.transform.localScale, new Vector3(spell.AreaRadius, spell.AreaRadius, 0.3f));
        cylinderInstance.transform.localScale = new_scale;

        // Ustawienie obiektu na warstwie nr 2, na której obiekty są ignorowane przez Raycast'a
        cylinderInstance.layer = 2;

        SetInstanceInLayer2(cylinderInstance);
    }
    
    // Ustawienie wszystkich podobiektów na warstwe nr 2
    private void SetInstanceInLayer2(GameObject instance)
    {
        foreach (Transform childTranform in instance.GetComponentsInChildren<Transform>())
        {
            childTranform.gameObject.layer = 2;
        }
    }
    
    // Zarządzanie rzucaniem zaklęć
    private void HandlePrepareCastingSpell()
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
                cylinderInstance.transform.localPosition = hit.point;
                // Jeśli kliknięty LPM to jest zatwierdzane rzucenie zaklęcia
                if (Input.GetMouseButtonDown(0))
                {                 
                    // Tworzenie instancji obiektu w miejscu kursora
                    castArea = Instantiate(cylinderInstance, hit.point, cylinderInstance.transform.rotation);                    

                    //zapisanie miejsca rzucania zaklęcia
                    spell.Position = castArea.transform.position;                    

                    // Utworzony obiekt jest ustawiany jako podobiekt "SpellsCaster"
                    castArea.transform.SetParent(this.transform);

                    // Potwierdzenie zakończenia przygotowania
                    preparedSpell = true;

                    Deacivate();
                }                
            }
        }
    }
    #endregion

    void Awake()
    {
        ice = new IceSpell();
        spellList.Add(ice);
    }


    void Update()
    {                            

        if (setArea)
        {
            HandlePrepareCastingSpell();
        }

        if(preparedSpell)
        {
            spell.Run();
            preparedSpell = false;
            workingSpell = true;
        }
        if(workingSpell)
        {
            workingSpell = spell.IsRunning;
            spell.DealDMG();
        }
        else
        {
            Destroy(castArea);
        }
    }
    private void OnCastTestSpell(InputValue inputAction)
    {
        Debug.Log("TestSpell");
        //ActivePrepareCastingSpell(0);
        
    }

    private void OnDeacivateSpell(InputValue inputAction)
    {
        //Deacivate();
        Debug.Log("DeactivateSpell");
    }


}
