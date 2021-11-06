using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed=4f;
    [SerializeField]
    float edgeSize=40f;
    private InputMaster controls;
    Vector3 horizontal , vertical;
    private bool isMoving;
    private Vector2 newXY;
    private void Awake()
    {
        controls = new InputMaster();
        controls.Enable();
        controls.Camera.movment.performed += Val => startMove(Val.ReadValue<Vector2>());
    }
    void Start()
    {
        
        vertical = Camera.main.transform.forward;
        vertical.y = 0;
        vertical = Vector3.Normalize(vertical); // normalizacja nie pamietam co to robi xD
        horizontal = Quaternion.Euler(new Vector3(0, 90, 0)) * vertical; // kopiowanie ustawien vertical ze znienionym katem dzialania

    }

    void Update()
    {
        if (isMoving) { CameraKeyMove(); }
        //return;
      
         if(Mouse.current.position.ReadValue().x >Screen.width-edgeSize)
        {
            CameraMouseMove(1,0); // krawedz prawa
        }
        else if (Mouse.current.position.ReadValue().x < edgeSize)
        {
            CameraMouseMove(-1,0);//krawedz lewa
        }
        else if (Mouse.current.position.ReadValue().y > Screen.height - edgeSize)
        {
            CameraMouseMove(0,1); // krawedz gorna 
        }
        else if (Mouse.current.position.ReadValue().y < edgeSize)
        {
            CameraMouseMove(0,-1);//krawedz dolna 
        }
    }
    /// <summary>
    /// poruszanie sie za pomoca strzałek zdefiniowanych w pojectSetting\inputMenager
    /// </summary>
    
    void CameraKeyMove()
    {
      //  Debug.Log("ruszam sie");
       // return;
        //Vector3 directed = new Vector3(Input.GetAxis("horizontalKey"), 0, Input.GetAxis("verticalKey"));
        Vector3 horizontalMove = horizontal * moveSpeed * Time.deltaTime * newXY.x;
        Vector3 verticalMove = vertical * moveSpeed * Time.deltaTime * newXY.y;
        //Debug.Log(Input.GetAxis("verticalKey"));
        //Vector3 heading = Vector3.Normalize(horizontalMove + verticalMove);

        // transform.forward = heading;
        transform.position += horizontalMove;
        transform.position += verticalMove;
    }
    void startMove(Vector2 xy)
    {
      //  Debug.Log(xy);
        isMoving = xy!=Vector2.zero;
        //if (!isMoving) { newXY = Vector2.zero;  }
        if (xy.x > 0) { newXY.x = 1; }
        else if (xy.x < 0) { newXY.x = -1; }
        else { newXY.x = 0; }
        if (xy.y > 0) { newXY.y = 1; }
        else if (xy.y < 0) { newXY.y = -1; }
        else { newXY.y = 0; }
        newXY = newXY.normalized;

    }

    /// <summary>
    /// poruszanie sie z pomoca myszy 
    /// </summary>
    /// <param name="directionX">kierunek poruszania sie w osi x</param>
    /// <param name="directionY">kierunek poruszania sie w osi y</param>
    void CameraMouseMove(short directionX,short directionY)
    {
        Vector3 horizontalMove = horizontal * moveSpeed * Time.deltaTime * directionX;
        Vector3 verticalMove = vertical * moveSpeed * Time.deltaTime * directionY;
        
        transform.position += horizontalMove;
        transform.position += verticalMove;

    }
}
