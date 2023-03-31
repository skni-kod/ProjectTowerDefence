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
    //
    //private InputMaster controls;
    Vector3 horizontal , vertical;
    public float maxX, minX, maxY, minY;
    private bool isMoving;
    private Vector2 newXY;
    private Rigidbody rb;
    //private void Awake()
    //{
    //    controls = new InputMaster();
    //    controls.Enable();
    //    controls.Camera.movment.performed += Val => startMove(Val.ReadValue<Vector2>());
    //}
    void Start()
    {
        
        vertical = Camera.main.transform.forward;
        vertical.y = 0;
        vertical = Vector3.Normalize(vertical); // normalizacja nie pamietam co to robi xD
        horizontal = Quaternion.Euler(new Vector3(0, 90, 0)) * vertical; // kopiowanie ustawien vertical ze znienionym katem dzialania
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        //return;
        Vector2 xy = new Vector2(0.0f,0.0f);
         if(Mouse.current.position.ReadValue().x >Screen.width-edgeSize)
        {
            // krawedz prawa
            xy += new Vector2(1.0f, 0.0f);
            isMoving = true;
        }
        if (Mouse.current.position.ReadValue().x < edgeSize)
        {
            //krawedz lewa
            xy += new Vector2(-1.0f, 0.0f);
            isMoving = true;
        }
        if (Mouse.current.position.ReadValue().y > Screen.height - edgeSize)
        {
            // krawedz gorna 
            xy += new Vector2(0.0f, 1.0f);
            isMoving = true;
        }
        if (Mouse.current.position.ReadValue().y < edgeSize)
        {
            //krawedz dolna 
            xy += new Vector2(0.0f, -1.0f);
            isMoving = true;
        }
        newXY = (newXY + xy.normalized)/2;
        newXY = newXY.normalized;
        isMoving = newXY != Vector2.zero;
        if (isMoving) { CameraKeyMove(); }
    }
    /// <summary>
    /// poruszanie sie za pomoca strzałek zdefiniowanych w pojectSetting\inputMenager
    /// </summary>
    
    void CameraKeyMove()
    {
      //  Debug.Log("ruszam sie");
       // return;
        //Vector3 directed = new Vector3(Input.GetAxis("horizontalKey"), 0, Input.GetAxis("verticalKey"));
        Vector3 horizontalMove = horizontal * moveSpeed  * newXY.x;
        Vector3 verticalMove = vertical * moveSpeed  * newXY.y;
        //Debug.Log(Input.GetAxis("verticalKey"));
        //Vector3 heading = Vector3.Normalize(horizontalMove + verticalMove);

        // transform.forward = heading;
        //if ((transform.position + horizontalMove).x < maxX && (transform.position + horizontalMove).x > minX) transform.position += horizontalMove;
        //if ((transform.position + verticalMove).z < maxY && (transform.position + verticalMove).z > minY) transform.position += verticalMove;

        rb.velocity = horizontalMove+verticalMove;

    }
    void OnMovment(InputValue inputValue)
    {
        //  Debug.Log(xy);
        
        Vector2 xy = inputValue.Get<Vector2>();
        //if (!isMoving) { newXY = Vector2.zero;  }
        newXY = xy.normalized;

    }

    /// <summary>
    /// poruszanie sie z pomoca myszy 
    /// </summary>
    /// <param name="directionX">kierunek poruszania sie w osi x</param>
    /// <param name="directionY">kierunek poruszania sie w osi y</param>
    void CameraMouseMove(short directionX,short directionY)
    {
        Vector3 horizontalMove = horizontal * moveSpeed * directionX;
        Vector3 verticalMove = vertical * moveSpeed * directionY;
        
    //if((transform.position+horizontalMove).x <maxX && (transform.position + horizontalMove).x > minX)   transform.position += horizontalMove;
    //if ((transform.position + verticalMove).z < maxY && (transform.position + verticalMove).z > minY) transform.position += verticalMove;

        rb.velocity = horizontalMove+verticalMove;
    }
}
