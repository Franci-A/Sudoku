using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Variable/Int")]
[Serializable]
public class IntScriptable : ScriptableObject
{
    public int value;

    public UnityEvent OnValueChanged;

    public void SetValue(int value)
    {
        this.value = value;
        OnValueChanged?.Invoke();
    }
}
