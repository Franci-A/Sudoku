using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ColorThemeHolder")]
public class SO_ThemeHolder : ScriptableObject
{
    public List<ThemeHolder> themes;
}

[Serializable]
public struct ThemeHolder
{
    public string name;
    public bool isUnlocked;
    public SO_ColorThemeScriptable theme;
}