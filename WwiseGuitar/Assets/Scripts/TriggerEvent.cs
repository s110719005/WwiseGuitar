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

    private Dictionary<Vector2, ButtonSetData> buttonEventDictionary;
    
    private void Awake() 
    {
        InitButtonEventDicionary();
    }
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

    private void InitButtonEventDicionary()
    {
        buttonEventDictionary = new Dictionary<Vector2, ButtonSetData>();
        foreach (var buttonSet in buttonSets)
        {
            Vector2 vector2 = Vector2.zero;
            vector2.x = buttonSet.ZValue;
            vector2.y = buttonSet.RZValue;
            if(!buttonEventDictionary.ContainsKey(vector2))
            {
                buttonEventDictionary.Add(vector2, buttonSet);
            }
            else
            {
                Debug.Log("button key" + buttonSet.eventName + "already exisit!");
            }
        }
    }

    
    private void CheckEvent()
    {
        Debug.Log("CurrentValue: " + guitarValue);
        if(buttonEventDictionary.TryGetValue(guitarValue, out ButtonSetData buttonSetData))
        {
            Debug.Log("Trigger Event: " + buttonSetData.eventName);
            //TriggerEvent(buttonSetData.eventIndex);
        }
    }
}
