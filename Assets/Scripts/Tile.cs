using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile
{
    public int solutionNumber = -1;
    public bool isFixedNumber = false;
    List<int> possibleNum;

    public int placedNumber { get; private set; }
    public UnityEvent onNumberUpdated = new UnityEvent();
    public List<GameObject> notes;
    [SerializeField] private SO_ColorThemeScriptable colorTheme;
    public int inSquareNum = -1;

    public Tile(SO_ColorThemeScriptable theme, int inSquareNum)
    {
        placedNumber = -1;
        possibleNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        colorTheme = theme;
        this.inSquareNum = inSquareNum;
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
        SetNumber(solutionNumber);
        isFixedNumber = true;
    }

    public bool SetNumber(int num)
    {
        if (isFixedNumber)
            return false;
        bool value = false;
        if (num == -1)
        {
            placedNumber = -1;
            onNumberUpdated.Invoke();
        }
        else {
            placedNumber = num;
            onNumberUpdated.Invoke();
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
        if (placedNumber != -1 || isFixedNumber)
            return;
        notes[number-1].SetActive(!notes[number-1].activeSelf);
    }

    public void ResetTile()
    {
        solutionNumber = -1;
        placedNumber = -1;
        isFixedNumber = false;
        onNumberUpdated?.Invoke();
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
