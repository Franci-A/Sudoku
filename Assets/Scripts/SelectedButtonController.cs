using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButtonController : MonoBehaviour
{
    Button[] buttons;
    [SerializeField] private IntScriptable selectedNumber;
    private Color baseColor;
    private int preivousNumber = -1;
    [SerializeField] private ColorThemeScriptable colorTheme;

    private void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        selectedNumber.value = 0;
        SelectedButton();
    }
    public void SelectedButton()
    {
        if (preivousNumber == selectedNumber.value -1)
            return;

        if(preivousNumber != -1)
            buttons[preivousNumber].image.color = baseColor;
        preivousNumber = selectedNumber.value -1;
        baseColor = buttons[selectedNumber.value].image.color;

        buttons[selectedNumber.value -1].image.color = colorTheme.selectedButton;
        
    }
}
