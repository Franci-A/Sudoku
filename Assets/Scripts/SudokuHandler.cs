using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SudokuHandler : MonoBehaviour
{
    public static SudokuHandler Instance { get; private set; }

    [SerializeField] private IntScriptable selectedNumber;
    [SerializeField] private IntScriptable difficultyNumber;
    [SerializeField] private BoolScriptable isNotes;
    [SerializeField] private BoolScriptable isGamePaused;

    [SerializeField] private SquareHandler[] squaresInstances;
    public List<List<Tile>> tiles;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private SO_ThemeHolder allTheme;
    private SO_ColorThemeScriptable colorTheme;
    private SudokuCreater sudokuCreater;
    
    public UnityEvent<int> OnAllNumPlaced = new UnityEvent<int>();
    public UnityEvent<int> OnNotAllNumPlaced = new UnityEvent<int>();

    private void Awake()
    {
        Instance = this;
        sudokuCreater = GetComponent<SudokuCreater>();
    }

    public void StartSudoku()
    {
        colorTheme = allTheme.GetSelectdedTheme();
        tiles = new List<List<Tile>>();
        for (int y = 0; y < 9; y++)
        {
            List<Tile> row = new List<Tile>();

            for (int x = 0; x < 9; x++)
            {
                int index = MathUtilities.ConvertGridToSquare(x,y);
                TileHandler obj = squaresInstances[index].GetTile((y % 3) * 3 + x % 3);
                Tile tile = new Tile(colorTheme, index);
                obj.Init(x, y, tile , colorTheme);
                tile.notes = obj.notes;
                row.Add(tile);
            }


            tiles.Add(row);
        }
        selectedNumber.SetValue(1);

        sudokuCreater.Init(difficultyNumber.value, tiles);

        SetStartingDifficulty();

        isGamePaused.SetValue(false);
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

    
    public void SetGrid(Vector2Int gridPos)
    {
        SetGrid(gridPos.x, gridPos.y);
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

                else
                {
                    int numPlaced = 0;
                    for (int i = 0; i < squaresInstances.Length; i++)
                    {
                        if (squaresInstances[i].HasNumber(selectedNumber.value))
                            numPlaced++;
                    }
                    if (numPlaced >= 9)
                    {
                        OnAllNumPlaced.Invoke(selectedNumber.value);
                    }
                    else
                    {
                        OnNotAllNumPlaced.Invoke(selectedNumber.value);
                    }
                }
            }
            //check for already place the same number in row/colunm/square
            if (CheckDoublesRow(gridX, gridY, selectedNumber.value))
            {
                squaresInstances[MathUtilities.ConvertGridToSquare(gridX, gridY)].GetTile(MathUtilities.ConvertGridToIndexInSquare(gridX, gridY)).SetWrongColor(true);
            }
            else if (CheckDoublesColunm(gridX, gridY, selectedNumber.value))
            {
                squaresInstances[MathUtilities.ConvertGridToSquare(gridX, gridY)].GetTile(MathUtilities.ConvertGridToIndexInSquare(gridX, gridY)).SetWrongColor(true);
            }
            else if (CheckDoublesSquare(gridX, gridY, selectedNumber.value))
            {
                squaresInstances[MathUtilities.ConvertGridToSquare(gridX, gridY)].GetTile(MathUtilities.ConvertGridToIndexInSquare(gridX, gridY)).SetWrongColor(true);
            }
            else
            {
                squaresInstances[MathUtilities.ConvertGridToSquare(gridX, gridY)].GetTile(MathUtilities.ConvertGridToIndexInSquare(gridX, gridY)).SetWrongColor(false);
            }
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
