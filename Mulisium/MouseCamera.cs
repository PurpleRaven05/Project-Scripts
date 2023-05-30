using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MouseCamera : MonoBehaviour
{
    public bool canMoveCamera { get; private set; } = true;

    //Components
    InputManager inputManager;
    public Transform playerBody;

    //Movement variables
    [Range(0.1f, 10f)] public float playerSens = 1f;
    float xRotation = 0f;

    //Inputs
    [SerializeField] private Vector2 mouseDirection { get { return inputManager.mouseDirectionVector; } set { } }

    
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputManager = GetComponentInParent<InputManager>();
    }

    void Update()
    {
        if (canMoveCamera)
        { 
            MouseMove();
        }
    }

    void MouseMove()
    {
        //OLD
        //mouseDirection = mouseDirection * Time.deltaTime * mouseSens * playerSens;
        //xRotation -= mouseDirection.y;
        //xRotation = Mathf.Clamp(xRotation, -70f, 90f);

        //playerBody.Rotate(Vector3.up * mouseDirection.x);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        mouseDirection = mouseDirection * playerSens;

        xRotation -= mouseDirection.y;
        xRotation = Mathf.Clamp(xRotation, -70f, 80f);

        playerBody.Rotate(Vector3.up * mouseDirection.x);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
