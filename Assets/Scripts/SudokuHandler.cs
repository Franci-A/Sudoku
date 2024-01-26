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
        selectedNumber.OnValueChanged.AddListener(HighlightSelected);
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
                obj.Init(x, y);
                tile.notes = obj.notes;
                tile.background = obj.background;
                row.Add(tile);
            }


            tiles.Add(row);
        }

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
        if (selectedNumber.value > 9)
        {
            tiles[gridY][gridX].SetNotesNumber(selectedNumber.value - 10);
        }
        else
        {
            tiles[gridY][gridX].SetNumber(selectedNumber.value);
            bool valid = tiles[gridY][gridX].CheckValidNumber();
            Debug.Log(valid);
            if (valid)
            {
                if (CheckPuzzle())
                {
                    victoryScreen.SetActive(true);
                    return;
                }
            }
            tiles[gridY][gridX].background.color = colorTheme.selectedBackground;
        }
    }

    public void HighlightSelected()
    {
        if (selectedNumber.value == -1)
            return;

        int number = selectedNumber.value;
        if (selectedNumber.value > 9)
            number -= 9;

        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                if ((tiles[y][x].fixedNumber && tiles[y][x].solutionNumber == number) ||
                    (!tiles[y][x].fixedNumber && tiles[y][x].placedNumber == number))
                {
                    tiles[y][x].background.color = colorTheme.selectedBackground;
                }
                else
                {
                    tiles[y][x].background.color = Color.white;
                }
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
