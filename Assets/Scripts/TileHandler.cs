using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileHandler : MonoBehaviour
{
    int x, y;
    public List<GameObject> notes;
    [SerializeField] private IntScriptable selectedNumber;
    public SpriteRenderer background;
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

    private void OnMouseDown()
    {
        SelectTile();
    }

    public void SelectTile()
    {
        background.sprite = colorTheme.selectedBackground;
        if (tile.fixedNumber)
        {
            selectedNumber.SetValue(tile.placedNumber);
            return;
        }
        SudokuController.Instance.OnSelectTile(x, y);
    }

    public void UnselectTile()
    {
        background.sprite = colorTheme.baseBackground;
    }

    private void HandleSelectedNumberColor()
    {
        if (selectedNumber.value == -1)
        {
            background.sprite = colorTheme.baseBackground;
            return;
        }

        if(selectedNumber.value == tile.placedNumber)
            background.sprite = colorTheme.selectedBackground;
        else
            background.sprite = colorTheme.baseBackground;
    }
}
