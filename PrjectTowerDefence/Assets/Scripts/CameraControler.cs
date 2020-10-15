using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class CameraControler : MonoBehaviour
{
    [SerializeField]
    float moveSpeed=4f;
    Vector3 horizontal , vertical;
    void Start()
    {
        vertical = Camera.main.transform.forward;
        vertical.y = 0;
        vertical = Vector3.Normalize(vertical);
        horizontal = Quaternion.Euler(new Vector3(0, 90, 0)) * vertical;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            Move();   
        }
    }
    void Move()
    {
        Vector3 directed = new Vector3(Input.GetAxis("horizontalKey"), 0, Input.GetAxis("verticalKey"));
        Vector3 horizontalMove = horizontal * moveSpeed * Time.deltaTime * Input.GetAxis("horizontalKey");
        Vector3 verticalMove = vertical * moveSpeed * Time.deltaTime * Input.GetAxis("verticalKey");

        //Vector3 heading = Vector3.Normalize(horizontalMove + verticalMove);

        // transform.forward = heading;
        transform.position += horizontalMove;
        transform.position += verticalMove;
    }
}
