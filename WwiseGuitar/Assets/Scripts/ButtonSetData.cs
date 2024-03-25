using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Button Set", menuName = "ButtonSet")]
[System.Serializable]
public class ButtonSetData : ScriptableObject
{
    public string eventName;
    public float ZValue;
    public float RZValue;
    //or wise id
    public int eventIndex;

}
