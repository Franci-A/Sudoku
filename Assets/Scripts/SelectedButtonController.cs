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
    [SerializeField] private SO_ThemeHolder themeHolder;
    [SerializeField] private SO_ColorThemeScriptable colorTheme;
    [SerializeField] private List<int> indexToButtonIndex;

    private void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        selectedNumber.value = 0;
        SelectedButton();
        colorTheme = themeHolder.GetSelectdedTheme();
        selectedNumber.OnValueChanged.AddListener(SelectedButton);
        baseColor = buttons[0].image.color;
    }

    public void SelectedButton()
    {
        if (selectedNumber.value == -1 || selectedNumber.value == 0)
            return;
        if (preivousNumber == indexToButtonIndex[selectedNumber.value - 1])
            return;

        if(preivousNumber != -1)
            buttons[preivousNumber].image.color = baseColor;
        preivousNumber = indexToButtonIndex[selectedNumber.value - 1];

        buttons[preivousNumber].image.color = colorTheme.selectedButton;
    }
}
