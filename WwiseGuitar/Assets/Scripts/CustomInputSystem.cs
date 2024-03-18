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
    private bool isZPressed;
    public bool IsZPressed => isZPressed;    
    private Vector2 rZInput;
    private bool isRZPressed;
    public bool IsRZPressed => isRZPressed;    
    public delegate void OnButtonStatusChangedDelegate(float value);
    public static event OnButtonStatusChangedDelegate OnRZPressed;
    public static event OnButtonStatusChangedDelegate OnZPressed;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else Instance = this;

        controls = new Controls();

        //controls.GuitarController.RZ.performed += OnRZInput;
        controls.GuitarController.RZ.started += OnRZInput;
        controls.GuitarController.RZ.canceled += OnRZInput;

        //controls.GuitarController.Z.performed += OnZInput;
        //controls.GuitarController.Z.started += OnZInput;
        //controls.GuitarController.Z.canceled += OnZInput;

        controls.GuitarController.Z.started += ctx => TestStart(ctx);
        controls.GuitarController.Z.canceled += ctx => TestCanceled(ctx);
    }

    private void TestCanceled(InputAction.CallbackContext ctx)
    {
        Debug.Log("Canceled: " + ctx);
        
    }
    private void TestStart(InputAction.CallbackContext ctx)
    {
        Debug.Log("start " + ctx);
    }
    private void OnZInput(InputAction.CallbackContext ctx)
    {
        zInput = ctx.ReadValue<Vector2>();
        Debug.Log(zInput);
        isZPressed = zInput.x != 0 || zInput.y != 0;
        //TODO: test it's x or y
        OnZPressed?.Invoke(zInput.y);
    }

    private void OnRZInput(InputAction.CallbackContext ctx)
    {
        rZInput = ctx.ReadValue<Vector2>();
        Debug.Log(rZInput);
        Debug.Log(ctx);
        isRZPressed = rZInput.x != 0 || rZInput.y != 0;
        //TODO: test it's x or y
        OnRZPressed?.Invoke(rZInput.y);
    }

    private void OnEnable() 
    {
        controls.GuitarController.Enable();
    }

    private void OnDisable() 
    {
        if(controls==null) { return; }
        controls.GuitarController.Disable();
    }
}
