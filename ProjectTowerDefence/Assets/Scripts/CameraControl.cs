using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //obniżanie CTRL
    //podwyższanie Space
    //E odblokowanie myszki
    private Rigidbody rb;
    public Transform orientation;
    public Camera mainCamera;

    public float movementSpeed = 4f;
    public float rotateSpeed = 4f;

    float xRotation;
    float yRotation;

    float mouseX;
    float mouseY;

    float horizontalMovement;
    float verticalMovement;
    Vector3 moveDirection;

    bool cameraEnabled = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (cameraEnabled)
        {
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");

            yRotation += mouseX * rotateSpeed;
            xRotation -= mouseY * rotateSpeed;
            orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
            mainCamera.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        }

        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

        rb.AddForce(moveDirection.normalized * movementSpeed, ForceMode.Acceleration);
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * movementSpeed, ForceMode.Acceleration);
        }
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            rb.AddForce(Vector3.down * movementSpeed, ForceMode.Acceleration);
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(cameraEnabled)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cameraEnabled = false;
            }
            else if(!cameraEnabled)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                cameraEnabled = true;
            }
        }
    }
}
