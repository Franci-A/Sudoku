using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SudokuCreater : MonoBehaviour
{
    public static SudokuCreater Instance { get; private set; }

    [SerializeField] private Transform parentTransform;
    [SerializeField] private TileHandler tilePrefab;
    private List<List<Tile>> tiles;
    [SerializeField] private IntScriptable selectedNumber;
    [SerializeField] private GameObject victoryScreen;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        tiles = new List<List<Tile>>();
        tiles = new List<List<Tile>>();
        for (int y = 0; y < 9; y++)
        {
            List<Tile> row = new List<Tile>();
            
            for (int x = 0; x < 9; x++)
            {
                Tile tile = new Tile();
                TileHandler obj = Instantiate(tilePrefab, parentTransform);
                obj.Init(x, y);
                tile.text = obj.GetComponentInChildren<TextMeshProUGUI>();

                row.Add(tile);
            }

            tiles.Add(row);
        }

        while (!SolvePuzzleRandomOrder()) 
        {
            ResetGrid();
        }
    }


    public bool SolvePuzzleRandomOrder()
    {
        List<int> index = new List<int>();
        for (int i = 0; i < 20; i++)
        {
            int rand;
            do
            {
                rand = UnityEngine.Random.Range(0, 81);
            }
            while (index.Contains(rand));
            index.Add(rand);
            Debug.Log(rand);
        }

        for (int j = 0; j < tiles.Count * tiles[0].Count; j++)
        {
            int indexX = -1;
            int indexY = -1;
            int numPossible = 10;
            for (int iy = 0; iy < tiles.Count; iy++)
            {
                for (int ix = 0; ix < tiles[iy].Count; ix++)
                {
                    if (tiles[iy][ix].solutionNumber != -1)
                        continue;

                    if (tiles[iy][ix].GetPossibleNumber.Count < numPossible)
                    {
                        indexX = ix;
                        indexY = iy;
                        numPossible = tiles[iy][ix].GetPossibleNumber.Count;
                    }

                }
            }
            

            if(indexX == -1|| numPossible == 0)
            {
                Debug.Log("Not possible");
                return false ;
            }

            tiles[indexY][indexX].SetRandomNumber();

            int currentNum = tiles[indexY][indexX].solutionNumber;

            if (index.Contains(j))
                tiles[indexY][indexX].SetStartNumber();

            for (int i = 0; i < 9; i++)
            {
                tiles[indexY][i].RemovePossibleNum(currentNum);

            }

            for (int i = 0; i < 9; i++)
            {
                tiles[i][indexX].RemovePossibleNum(currentNum);
            }

            int gridX = indexX / 3;
            int gridY = indexY / 3;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    tiles[gridY *3 +y][gridX *3 +x].RemovePossibleNum(currentNum);

                }
            }

        }
        return true;
    }

    public void SetGrid(int gridX, int gridY)
    {
        tiles[gridY][gridX].SetNumber(selectedNumber.GetValue);
        if (CheckPuzzle())
        {
           victoryScreen.SetActive(true);
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