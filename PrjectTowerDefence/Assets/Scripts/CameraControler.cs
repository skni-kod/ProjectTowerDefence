using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class CameraControler : MonoBehaviour
{
    [SerializeField]
    float moveSpeed=4f;
    [SerializeField]
    float edgeSize=40f;

    Vector3 horizontal , vertical;
    void Start()
    {
        vertical = Camera.main.transform.forward;
        vertical.y = 0;
        vertical = Vector3.Normalize(vertical);
        horizontal = Quaternion.Euler(new Vector3(0, 90, 0)) * vertical;

    }

    void Update()
    {
        if(Input.anyKey)
        {
            CmaeraKeyMove();
        }
        if(Input.mousePosition.x >Screen.width-edgeSize)
        {
            CmaeraMouseMove(1,0);
        }
        if (Input.mousePosition.x < edgeSize)
        {
            CmaeraMouseMove(-1,0);
        }
        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            CmaeraMouseMove(0,1);
        }
        if (Input.mousePosition.y < edgeSize)
        {
            CmaeraMouseMove(0,-1);
        }
    }

    void CmaeraKeyMove()
    {
        //Vector3 directed = new Vector3(Input.GetAxis("horizontalKey"), 0, Input.GetAxis("verticalKey"));
        Vector3 horizontalMove = horizontal * moveSpeed * Time.deltaTime * Input.GetAxis("horizontalKey");
        Vector3 verticalMove = vertical * moveSpeed * Time.deltaTime * Input.GetAxis("verticalKey");
        //Debug.Log(Input.GetAxis("verticalKey"));
        //Vector3 heading = Vector3.Normalize(horizontalMove + verticalMove);

        // transform.forward = heading;
        transform.position += horizontalMove;
        transform.position += verticalMove;
    }
    void CmaeraMouseMove(short directionX,short directionY)
    {
        Vector3 horizontalMove = horizontal * moveSpeed * Time.deltaTime * directionX;
        Vector3 verticalMove = vertical * moveSpeed * Time.deltaTime * directionY;
        
        transform.position += horizontalMove;
        transform.position += verticalMove;

    }
}
