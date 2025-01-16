using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ColorTheme")]
[Serializable]
public class SO_ColorThemeScriptable : ScriptableObject
{
    public Color selectedButton = Color.white;
    public Sprite baseBackground;
    public Sprite highlightedBackground;
    public Sprite selectedBackground;
    public Color fixedTextColor = Color.white; //text
    public Color placedTextColor = Color.white;
    public Color wrongTextColor = Color.red;
}
