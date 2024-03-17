using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomInputSystem : MonoBehaviour
{
    public static CustomInputSystem Instance;
    private Controls controls;

    private Vector2 zInput;
    private Vector2 zRInput;
    private Vector2 currentZRInput;
    private bool isZRPressed;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else Instance = this;

        controls = new Controls();

        controls.GuitarController.RZ.performed += OnRZInput;
        controls.GuitarController.RZ.started += OnRZInput;
        controls.GuitarController.RZ.canceled += OnRZInput;

        Debug.Log("INPUT SYSYEM INIT");
    }

    private void OnRZInput(InputAction.CallbackContext ctx)
    {
        zRInput = ctx.ReadValue<Vector2>();
        currentZRInput = zRInput;
        Debug.Log(zRInput);
        isZRPressed = zRInput.x != 0 || zRInput.y != 0;
    }

    private void OnEnable() 
    {
        controls.GuitarController.Enable();
    }

    private void OnDisable() 
    {
        if(controls==null){
            return;
        }
        controls.GuitarController.Disable();
    }
}
