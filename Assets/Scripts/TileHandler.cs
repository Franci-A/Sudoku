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
    [SerializeField] private IntScriptable selectedNumber;
    public Tile tile;
    private SO_ColorThemeScriptable colorTheme;

    public void Init(int gridX, int gridY, Tile tile, SO_ColorThemeScriptable theme)
    {
        x = gridX;
        y = gridY;
        this.tile = tile;
        colorTheme = theme;
        selectedNumber.OnValueChanged.AddListener(HandleSelectedNumberColor);
    }

    public void SetNumber()
    {
        SudokuHandler.Instance.SetGrid(x, y);
    }

    private void HandleSelectedNumberColor()
    {
        if (selectedNumber.value == -1)
        {
            tile.background.color = Color.white;
            return;
        }

        if(selectedNumber.value == tile.placedNumber)
            tile.background.color = colorTheme.selectedBackground;
        else
            tile.background.color = Color.white;
    }
}
