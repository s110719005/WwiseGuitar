using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    //x = Z, y = RZ
    private Vector2 guitarValue = Vector2.zero;
    [SerializeField]
    private List<ButtonSetData> buttonSets;
    
    private void OnEnable() 
    {
        CustomInputSystem.OnRZPressed += ChangedRZValue;
        CustomInputSystem.OnZPressed += ChangedZValue;
    }

    private void OnDisable()
    {
        CustomInputSystem.OnRZPressed -= ChangedRZValue;
        CustomInputSystem.OnZPressed -= ChangedZValue;
    }
    private void ChangedZValue(float newValue)
    {
        guitarValue.x = newValue;
        CheckEvent();
    }

    private void ChangedRZValue(float newValue)
    {
        guitarValue.y = newValue;
        CheckEvent();
    }

    
    private void CheckEvent()
    {
        foreach (var buttonSet in buttonSets)
        {
            if(guitarValue.x == buttonSet.ZValue && guitarValue.y == buttonSet.RZValue)
            {
                Debug.Log("Trigger Event: " + buttonSet.eventName);
            }
        }
    }
}
