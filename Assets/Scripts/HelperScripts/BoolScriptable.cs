using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Variable/Bool")]
[Serializable]
public class BoolScriptable : ScriptableObject
{
    public bool value;

    public UnityEvent OnValueChanged;

    public void SetValue(bool value)
    {
        this.value = value;
        OnValueChanged?.Invoke();
    }
    
    public void ToggleValue()
    {
        this.value = !value;
        OnValueChanged?.Invoke();
    }
}
