using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile
{
    public int solutionNumber = -1;
    public bool fixedNumber = false;
    List<int> possibleNum;

    public int placedNumber = -1;
    public TextMeshProUGUI text;
    public List<GameObject> notes;
    public Image background;
    [SerializeField] private SO_ColorThemeScriptable colorTheme;

    public Tile(SO_ColorThemeScriptable theme, TextMeshProUGUI textInstance)
    {
        possibleNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        colorTheme = theme;
        text = textInstance;
        text.color = colorTheme.placedColor;
    }

    public List<int> GetPossibleNumber => possibleNum;

    public bool RemovePossibleNum(int num)
    {
        if (placedNumber != -1)
            return false;

        if (possibleNum.Count <= 0)
            return false;

        if (!possibleNum.Contains(num))
            return false;

        possibleNum.Remove(num);
        if (possibleNum.Count == 1)
        {
            return true;
        }
        return false;

    }

    public void SetSolutionNumber(int num)
    {
        solutionNumber = num;
    }

    public void SetStartNumber()
    {
        text.color = colorTheme.fixedColor;
        SetNumber(solutionNumber);
        fixedNumber = true;
    }

    public bool SetNumber(int num)
    {
        if (fixedNumber)
            return false;
        bool value = false;
        if (num == -1)
        {
            placedNumber = -1;
            text.text = "";
        }
        else {
            placedNumber = num;
            text.text = num.ToString(); 
            value = true;
        }
        for (int i = 0; i < notes.Count; i++)
        {
            notes[i].SetActive(false);
        }
        return value;
    }

    public void SetRandomNumber()
    {
        int rand = Random.Range(0, possibleNum.Count);
        SetSolutionNumber(possibleNum[rand]);
    }

    public void SetPossibleNumber()
    {
        if (possibleNum.Count == 0 || possibleNum.Count > 1)
            return;
        SetSolutionNumber(possibleNum[0]);
    }
    
    public void SetNotesNumber(int number)
    {
        if (placedNumber != -1 || fixedNumber)
            return;
        notes[number].SetActive(!notes[number].activeSelf);
    }

    public void ResetTile()
    {
        solutionNumber = -1;
        placedNumber = -1;
        fixedNumber = false;
        possibleNum.Clear();
        possibleNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        for (int i = 0; i < notes.Count; i++)
        {
            notes[i].SetActive(false);
        }
    }

    public bool CheckValidNumber()
    {
        return placedNumber == solutionNumber;
    }
}
