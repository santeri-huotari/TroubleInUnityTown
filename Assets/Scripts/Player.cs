using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    private CharacterController characterController;
    private GameObject cameraObject;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpSpeed = 5f;

    private float rotationSpeed = 500f;

    private float gravity = 9.8f;
    float vSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        cameraObject = gameObject.GetComponentInChildren<Camera>().gameObject;

        if (!isLocalPlayer)
        {
            cameraObject.GetComponent<Camera>().enabled = false;
            cameraObject.GetComponent<AudioListener>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            Movement();
            Rotation();
        }
    }

    void Movement()
    {
        if (characterController.isGrounded)
        {
            vSpeed = 0f;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space))
            {
                vSpeed = jumpSpeed;
            }
        }
        else
        {
            vSpeed -= gravity * Time.deltaTime;
        }

        Vector3 movement = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0) * new Vector3(Input.GetAxis("Horizontal"), vSpeed, Input.GetAxis("Vertical")) * Time.deltaTime * speed;
        characterController.Move(movement);
    }

    void Rotation()
    {
        Vector3 lookVector = new Vector3(-Input.GetAxis("Mouse Y"), 0, 0) * Time.deltaTime * rotationSpeed;
        Vector3 rotationVector = new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotationSpeed;
        cameraObject.transform.Rotate(lookVector);
        transform.Rotate(rotationVector);
    }
}
