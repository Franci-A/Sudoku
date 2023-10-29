using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileHandler : MonoBehaviour
{
    int x, y;
    public List<GameObject> notes;
    public Image background;

    public void Init(int gridX, int gridY)
    {
        x = gridX;
        y = gridY;
    }

    public void SetNumber()
    {
        SudokuHandler.Instance.SetGrid(x, y);
    }
}
