using UnityEngine;

public class JohnScript : MonoBehaviour
{
    private Rigidbody rb;
    private Transform orientation;
    public float movementSpeed = 4f;
    public float rotateSpeed = 4f;

    float yRotation;
    float mouseX;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        orientation = GetComponent<Transform>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        yRotation += mouseX * rotateSpeed;

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.forward * movementSpeed, ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.forward * -movementSpeed, ForceMode.Acceleration);
        }
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
