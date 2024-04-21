using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    //x = Z, y = RZ
    private Vector2 guitarValue = Vector2.zero;
    [Header("Single Event")]
    [SerializeField]
    private AK.Wwise.Event singleEvent;
    [Header("Multiple Events")]
    [SerializeField]
    private List<ButtonSetData> multipleEventsButtonSets;

    private Dictionary<Vector2, ButtonSetData> multipleButtonEventDictionary;
    //DEBUG
    // [SerializeField]
    // private ButtonSetData DEBUG_testData;
    
    private void Awake() 
    {
        InitButtonEventDicionary();
    }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.E))
        // {
        //     float value = -0.9957108f;
        //     value = (int)(Math.Round(value, 3)*1000);
        //     ChangedZValue(value);
        // }
        // if(Input.GetKeyDown(KeyCode.F))
        // {
        //     float value = -0.9859068f;
        //     value = (int)(Math.Round(value, 3)*1000);
        //     ChangedZValue(value);
        // }
        // if(Input.GetKeyDown(KeyCode.G))
        // {
        //     float value = -0.9761029f;
        //     value = (int)(Math.Round(value, 3)*1000);
        //     ChangedZValue(value);
        // }
        // if(Input.GetKeyDown(KeyCode.L))
        // {
        //     DEBUG_testData.wiseEvent.Post(gameObject);
        // }
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
        multipleButtonEventDictionary = new Dictionary<Vector2, ButtonSetData>();
        foreach (var buttonSet in multipleEventsButtonSets)
        {
            Vector2 vector2 = Vector2.zero;
            vector2.x = (int)(Math.Round(buttonSet.ZValue, 3)*1000);
            vector2.y = (int)(Math.Round(buttonSet.RZValue, 3)*1000);
            //vector2.y = buttonSet.RZValue;
            if(!multipleButtonEventDictionary.ContainsKey(vector2))
            {
                multipleButtonEventDictionary.Add(vector2, buttonSet);
            }
            else
            {
                Debug.Log("button key" + buttonSet.eventName + "already exisit!");
            }
        }
    }

    
    private void CheckEvent()
    {
        Debug.Log("CurrentValue: " + guitarValue.x + ", " + guitarValue.y);
        if(CustomInputSystem.Instance.CurrentInputType == CustomInputSystem.InputType.singleEvent)
        {
            singleEvent.Post(gameObject);
        }
        else if(CustomInputSystem.Instance.CurrentInputType == CustomInputSystem.InputType.multipleEvents)
        {
            if(multipleButtonEventDictionary.TryGetValue(guitarValue, out ButtonSetData buttonSetData))
            {
                Debug.Log("Trigger Event: " + buttonSetData.eventName);
                if(buttonSetData.wiseEvent != null)
                {
                    buttonSetData.wiseEvent.Post(gameObject);
                }
            }
        }
    }
}
