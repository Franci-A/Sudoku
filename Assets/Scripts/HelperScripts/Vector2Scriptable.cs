using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Variable/Vector2 int")]
[Serializable]
public class Vector2Scriptable : ScriptableObject
{
    public Vector2Int value;

    public UnityEvent OnValueChanged;

    public void SetValue(int x, int y)
    {
        SetValue(new Vector2Int(x, y));
    }
    
    public void SetValue(Vector2Int value)
    {
        this.value = value;
        OnValueChanged?.Invoke();
    }
}