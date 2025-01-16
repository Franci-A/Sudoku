using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ColorThemeHolder")]
public class SO_ThemeHolder : ScriptableObject
{
    public List<ThemeHolder> themes;
    public VectorScriptable selectedTheme;

    public SO_ColorThemeScriptable GetSelectdedTheme()
    {        
        return themes.Find(x => x.themeId == selectedTheme.value).theme;
    }
}

[Serializable]
public struct ThemeHolder
{
    public string name;
    public bool isUnlocked;
    public int themeId;
    public SO_ColorThemeScriptable theme;
}