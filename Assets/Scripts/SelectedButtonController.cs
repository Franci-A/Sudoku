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
    List<int> completedNumber = new List<int>();

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
        if (preivousNumber == selectedNumber.value - 1)
            return;

        if (preivousNumber != -1)
        {
            buttons[indexToButtonIndex[preivousNumber]].image.color = completedNumber.Contains(preivousNumber)? colorTheme.completedButton : baseColor;
        }
        preivousNumber = selectedNumber.value - 1;

        buttons[indexToButtonIndex[preivousNumber]].image.color = colorTheme.selectedButton;
    }

    public void CompletedNumber(int number)
    {
        buttons[indexToButtonIndex[number-1]].image.color = colorTheme.completedButton;
        completedNumber.Add(number-1);
    }

    public void NotCompletedNumber(int number)
    {
        if (completedNumber.Contains(number-1))
        {
            completedNumber.Remove(number-1);
            buttons[indexToButtonIndex[number-1]].image.color = baseColor;
        }
    }
}
