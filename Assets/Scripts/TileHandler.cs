using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    int x, y;

    public void Init(int gridX, int gridY)
    {
        x = gridX;
        y = gridY;
    }

    public void SetNumber()
    {
        Debug.Log(x + " " + y);
        SudokuCreater.Instance.SetGrid(x, y);
    }
}
