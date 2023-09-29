using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ColorTheme")]
[Serializable]
public class ColorThemeScriptable : ScriptableObject
{
    public Color selectedButton = Color.white;
    public Color selectedBackground= Color.white;
    public Color fixedColor= Color.white;
    public Color placedColor = Color.white;
}
