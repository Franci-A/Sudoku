using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile
{
    public int solutionNumber = -1;
    bool fixedNumber = false;
    List<int> possibleNum;

    public int placedNumber = -1;
    public TextMeshProUGUI text;

    public Tile()
    {
        possibleNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }

    public List<int> GetPossibleNumber => possibleNum;

    public bool RemovePossibleNum(int num)
    {
        if (solutionNumber != -1 && num == solutionNumber)
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
        text.color = Color.blue;
        SetNumber(solutionNumber);
        fixedNumber = true;
    }

    public void SetNumber(int num)
    {
        if (fixedNumber)
            return;
        if (num == -1)
        {
            placedNumber = -1;
            text.text = "";
        }
        else {
            placedNumber = num;
            text.text = num.ToString(); 
        }
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

    public void ResetTile()
    {
        solutionNumber = -1;
        possibleNum.Clear();
        possibleNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }

    public bool CheckValidNumber()
    {
        return placedNumber == solutionNumber;
    }
}
