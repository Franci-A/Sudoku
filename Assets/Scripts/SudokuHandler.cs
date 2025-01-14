using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SudokuHandler : MonoBehaviour
{
    public static SudokuHandler Instance { get; private set; }

    [SerializeField] private IntScriptable selectedNumber;
    [SerializeField] private IntScriptable difficultyNumber;
    [SerializeField] private BoolScriptable isNotes;


    [SerializeField] private Transform parentTransform;
    [SerializeField] private TileHandler tilePrefab;
    private List<List<Tile>> tiles;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private SO_ThemeHolder allTheme;
    private SO_ColorThemeScriptable colorTheme;
    private SudokuCreater sudokuCreater;

    private void Awake()
    {
        Instance = this;
        sudokuCreater = GetComponent<SudokuCreater>();
    }

    private void Start()
    {
        colorTheme = allTheme.GetSelectdedTheme();
        tiles = new List<List<Tile>>();
        for (int y = 0; y < 9; y++)
        {
            List<Tile> row = new List<Tile>();

            for (int x = 0; x < 9; x++)
            {
                TileHandler obj = Instantiate(tilePrefab, parentTransform);
                Tile tile = new Tile(colorTheme, obj.GetComponentInChildren<TextMeshProUGUI>());
                obj.Init(x, y, tile , colorTheme);
                tile.notes = obj.notes;
                tile.background = obj.background;
                row.Add(tile);
            }


            tiles.Add(row);
        }
        selectedNumber.SetValue(1);

        sudokuCreater.Init(difficultyNumber.value, tiles);

        SetStartingDifficulty();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            sudokuCreater.Init(difficultyNumber.value, tiles);
        }
    }


    private void SetStartingDifficulty()
    {
        List<int> index = new List<int>();
        for (int i = 0; i < difficultyNumber.value; i++)
        {
            int rand;
            do
            {
                rand = UnityEngine.Random.Range(0, 81);
            }
            while (index.Contains(rand));
            index.Add(rand);
        }

        for (int i = 0; i < index.Count; i++)
        {
            tiles[index[i] / 9][index[i] % 9].SetStartNumber();

        }
    }

    public void SetGrid(int gridX, int gridY)
    {
        if (selectedNumber.value == 0)
            return;
        if (isNotes.value)
        {
            tiles[gridY][gridX].SetNotesNumber(selectedNumber.value);
        }
        else
        {
            bool placed = tiles[gridY][gridX].SetNumber(selectedNumber.value);
            bool valid = tiles[gridY][gridX].CheckValidNumber();
            if (valid)
            {
                if (CheckPuzzle())
                {
                    victoryScreen.SetActive(true);
                    return;
                }
            }
            //check for already place the same number in row/colunm/square
            if(CheckDoublesRow(gridX, gridY, selectedNumber.value))
            {
                tiles[gridY][gridX].SetNumberColor(colorTheme.wrongColor);
            }
            if(CheckDoublesColunm(gridX, gridY, selectedNumber.value))
            {
                tiles[gridY][gridX].SetNumberColor(colorTheme.wrongColor);
            }
            if(CheckDoublesSquare(gridX, gridY, selectedNumber.value))
            {
                tiles[gridY][gridX].SetNumberColor(colorTheme.wrongColor);
            }
            if (placed) 
                tiles[gridY][gridX].background.color = colorTheme.selectedBackground;
        }
    }


    public bool CheckPuzzle()
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if (!tiles[y][x].CheckValidNumber())
                    return false;
            }
        }
        return true;
    }
    
    public bool CheckDoublesRow(int xValue, int yValue, int placedValue)
    {
        for (int x = 0; x < 9; x++)
        {
            if (xValue == x)
                continue;

            if (tiles[yValue][x].placedNumber == placedValue)
                return true;
        }
        return false;
    }
    public bool CheckDoublesColunm(int xValue, int yValue, int placedValue)
    {
        for (int y = 0; y < 9; y++)
        {
            if (yValue == y)
                continue;

            if (tiles[y][xValue].placedNumber == placedValue)
                return true;
        }
        return false;
    }
    
    public bool CheckDoublesSquare(int xValue,int yValue, int placedValue)
    {
        int baseY = yValue - yValue % 3;
        int baseX = xValue - xValue % 3;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (baseX + x == xValue && baseY + y == yValue)
                    continue;

                if (tiles[baseY + y][baseX + x].placedNumber == placedValue)
                    return true;
            }
        }
        return false;
    }

    public void ResetGrid()
    {
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                tiles[y][x].ResetTile();
            }
        }
    }

}
