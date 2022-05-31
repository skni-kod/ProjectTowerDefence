using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DestroyBuildings : MonoBehaviour
{
    public Camera currentCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(currentCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        foreach (RaycastHit hit in hits)
        {
            // Sprawdza czy trafiony obiekt ma ustawiony tag "Terrain"
            if (hit.collider.CompareTag("Building"))
            {
                print(hit);
                Destroy(hit.transform.gameObject);
            }
            else
            {

            }
        }
    }
}
