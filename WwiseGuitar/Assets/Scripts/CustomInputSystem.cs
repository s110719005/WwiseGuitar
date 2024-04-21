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
    [SerializeField]
    private InputType inputType;
    public InputType CurrentInputType => inputType;
    public delegate void OnButtonStatusChangedDelegate(float value);
    public static event OnButtonStatusChangedDelegate OnRZPressed;
    public static event OnButtonStatusChangedDelegate OnZPressed;
    
    public enum InputType
    {
        singleEvent = 0,
        multipleEvents = 1
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else Instance = this;

        controls = new Controls();

        controls.GuitarController.Z.performed += ctx => ZPerformed(ctx);
        controls.GuitarController.Z.started += ctx => ZStart(ctx);
        controls.GuitarController.Z.canceled += ctx => ZCanceled(ctx);

        controls.GuitarController.RZ.performed += ctx => RZPerformed(ctx);
        controls.GuitarController.RZ.started += ctx => RZStart(ctx);
        controls.GuitarController.RZ.canceled += ctx => RZCanceled(ctx);
    }

    private void ZPerformed(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        value = (int)(Math.Round(value, 3)*1000);
        Debug.Log("Performed: " + value);
        OnZPressed.Invoke(value);
    }

    private void ZCanceled(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        //Debug.Log("Canceled: " + value);
        OnZPressed.Invoke(0);
    }
    private void ZStart(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        //Debug.Log("Start: " + value);
    }
    private void RZPerformed(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        value = (int)(Math.Round(value, 3)*1000);
        Debug.Log("Performed: " + value);
        OnRZPressed.Invoke(value);
    }

    private void RZCanceled(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        //Debug.Log("Canceled: " + value);
        OnRZPressed.Invoke(0);
    }
    private void RZStart(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        //Debug.Log("Start: " + value);
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
