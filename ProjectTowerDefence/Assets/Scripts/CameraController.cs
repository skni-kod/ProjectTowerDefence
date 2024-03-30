using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 4f;
    [SerializeField]
    float edgeSize = 40f;
    //
    //private InputMaster controls;
    Vector3 horizontal, vertical;
    public float maxX, minX, maxY, minY;
    private bool isMoving;
    private Vector2 newXY;
    private Vector2 keyboardXY;
    private Rigidbody rb;
    [SerializeField] private bool showMapEdges = true;
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
        newXY = (keyboardXY + DetectCameraEdges()) / 2;
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
        Vector3 horizontalMove = horizontal * moveSpeed * newXY.x;
        Vector3 verticalMove = vertical * moveSpeed * newXY.y;

        //Debug.Log(Input.GetAxis("verticalKey"));
        //Vector3 heading = Vector3.Normalize(horizontalMove + verticalMove);
        horizontalMove = (transform.position + horizontalMove).z < maxX && (transform.position + horizontalMove).z > minX ? horizontalMove : Vector3.zero;
        verticalMove = (transform.position + verticalMove).x < maxX && (transform.position + verticalMove).x > minX ? verticalMove : Vector3.zero;

        // transform.forward = heading;
        //if ((transform.position + horizontalMove).x < maxX && (transform.position + horizontalMove).x > minX) transform.position += horizontalMove;
        //if ((transform.position + verticalMove).z < maxY && (transform.position + verticalMove).z > minY) transform.position += verticalMove;

        rb.velocity = (horizontalMove + verticalMove).normalized * moveSpeed;

    }

    Vector2 DetectCameraEdges()
    {
        float x = 0.0f;
        float y = 0.0f;
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        //check to prevent camera movement on the start of the scene if mouse hasn't been moved.
        if(mousePosition.Equals(Vector2.zero))
            return new Vector2(x, y);

        if (mousePosition.x > Screen.width - edgeSize)
        {
            // krawedz prawa
            x = 1.0f;
            //xy += new Vector2(1.0f, 0.0f);
        }
        else if (mousePosition.x < edgeSize)
        {
            //krawedz lewa
            x = -1.0f;
            //xy += new Vector2(-1.0f, 0.0f);
        }
        else
        {
            x = 0.0f;
        }

        if (mousePosition.y > Screen.height - edgeSize)
        {
            // krawedz gorna
            y = 1.0f;
            //xy += new Vector2(0.0f, 1.0f);
        }
        else if (mousePosition.y < edgeSize)
        {
            //krawedz dolna 
            y = -1.0f;
            //xy += new Vector2(0.0f, -1.0f);
        }
        else
        {
            y = 0.0f;
        }

        return new Vector2(x, y);
    }

    void OnMovment(InputValue inputValue)
    {
        //  Debug.Log(xy);

        keyboardXY = inputValue.Get<Vector2>();
        //if (!isMoving) { newXY = Vector2.zero;  }
    }

    /// <summary>
    /// poruszanie sie z pomoca myszy 
    /// </summary>
    /// <param name="directionX">kierunek poruszania sie w osi x</param>
    /// <param name="directionY">kierunek poruszania sie w osi y</param>
    void CameraMouseMove(short directionX, short directionY)
    {
        Vector3 horizontalMove = horizontal * moveSpeed * directionX;
        Vector3 verticalMove = vertical * moveSpeed * directionY;

        //if((transform.position+horizontalMove).x <maxX && (transform.position + horizontalMove).x > minX)   transform.position += horizontalMove;
        //if ((transform.position + verticalMove).z < maxY && (transform.position + verticalMove).z > minY) transform.position += verticalMove;

        rb.velocity = horizontalMove + verticalMove;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!showMapEdges)
            return;

        Gizmos.color = Color.yellow;

        float width = maxX - minX;
        float depth = maxY - minY;

        Vector3 center = new Vector3((minX + maxX) / 2f, 0, (minY + maxY) / 2f);
        Vector3 size = new Vector3(width, 100, depth);

        Gizmos.DrawWireCube(center, size);
    }
#endif
}