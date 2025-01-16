using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TileHandler : MonoBehaviour
{
    int x, y;
    public List<GameObject> notes;
    [SerializeField] private IntScriptable selectedNumber;
    [SerializeField] private Vector2Scriptable selectedTile;
    public SpriteRenderer background;
    private TextMeshPro numText;
    public Tile tile;
    private SO_ColorThemeScriptable colorTheme;

    private void Awake()
    {
        numText = GetComponentInChildren<TextMeshPro>();
    }
    public void Init(int gridX, int gridY, Tile tile, SO_ColorThemeScriptable theme)
    {
        x = gridX;
        y = gridY;
        this.tile = tile;
        colorTheme = theme;
        selectedNumber.OnValueChanged.AddListener(HandleSelectedNumberColor);
        selectedTile.OnValueChanged.AddListener(HandleSelectedTileColor);
        numText.color = tile.isFixedNumber ? colorTheme.fixedTextColor : colorTheme.placedTextColor;
        this.tile.onNumberUpdated.AddListener(UpdateText);
    }

    private void OnDestroy()
    {
        this.tile.onNumberUpdated.RemoveListener(UpdateText);
        selectedNumber.OnValueChanged.RemoveListener(HandleSelectedNumberColor);
        selectedTile.OnValueChanged.RemoveListener(HandleSelectedTileColor);
    }
    private void OnMouseDown()
    {
        SelectTile();
    }

    public void SelectTile()
    {
        selectedTile.SetValue(x, y);
        if (tile.isFixedNumber)
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
            //background.sprite = colorTheme.baseBackground;
            return;
        }

        if(selectedNumber.value == tile.placedNumber)
            background.sprite = colorTheme.selectedBackground;
       /* else
            background.sprite = colorTheme.baseBackground;*/
    }

    private void HandleSelectedTileColor()
    {
        if (selectedTile.x == -1)
        {
            background.sprite = colorTheme.baseBackground;
            return;
        }

        if (selectedTile.y == y && selectedTile.x == x)
        {
            //is selected tile
            background.sprite = colorTheme.selectedBackground;

        }
        else if (selectedTile.y == y || selectedTile.x == x || tile.inSquareNum == MathUtilities.ConvertGridToSquare(selectedTile.x, selectedTile.y))
        {
            //is in row or column or square
            background.sprite = colorTheme.highlightedBackground;
        }
        else
        {
            background.sprite = colorTheme.baseBackground;
        }
    }

    private void UpdateText()
    {
        numText.text = tile.placedNumber == -1 ? "" : tile.placedNumber.ToString();
    }

    public void SetWrongColor(bool isWrong)
    {
        if (isWrong)
        {
            numText.color = colorTheme.wrongTextColor;

        }
        else
        {
            numText.color = tile.isFixedNumber ? colorTheme.fixedTextColor : colorTheme.placedTextColor;
        }
    }
}
