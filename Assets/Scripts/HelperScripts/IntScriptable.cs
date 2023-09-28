using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Variable/Int")]

public class IntScriptable : ScriptableObject
{
    private int value;

    public int GetValue => value;
    public void SetValue(int value)
    {
        this.value = value;
    }
}
