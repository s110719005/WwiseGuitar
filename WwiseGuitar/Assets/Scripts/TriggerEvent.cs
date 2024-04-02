using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            float value = -0.9957108f;
            value = (int)(Math.Round(value, 3)*1000);
            ChangedZValue(value);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            float value = -0.9859068f;
            value = (int)(Math.Round(value, 3)*1000);
            ChangedZValue(value);
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            float value = -0.9761029f;
            value = (int)(Math.Round(value, 3)*1000);
            ChangedZValue(value);
        }
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
            vector2.x = (int)(Math.Round(buttonSet.ZValue, 3)*1000);
            vector2.y = (int)(Math.Round(buttonSet.RZValue, 3)*1000);
            //vector2.y = buttonSet.RZValue;
            if(!buttonEventDictionary.ContainsKey(vector2))
            {
                buttonEventDictionary.Add(vector2, buttonSet);
            }
            else
            {
                Debug.Log("button key" + buttonSet.eventName + "already exisit!");
            }
        }
        for(int i = 0; i < buttonEventDictionary.Count; i++)
        {
            Debug.Log(buttonEventDictionary.ElementAt(i).Key.x +", " +  buttonEventDictionary.ElementAt(i).Key.y + " Event: " + buttonEventDictionary.ElementAt(i).Value.eventName);
        }
    }

    
    private void CheckEvent()
    {
        Debug.Log("CurrentValue: " + guitarValue.x + ", " + guitarValue.y);
        if(buttonEventDictionary.TryGetValue(guitarValue, out ButtonSetData buttonSetData))
        {
            Debug.Log("Trigger Event: " + buttonSetData.eventName);
            //TriggerEvent(buttonSetData.eventIndex);
        }
    }
}
