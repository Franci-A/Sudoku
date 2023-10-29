using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class SudokuCreater : MonoBehaviour
{
    public void Init(int startingNumbers, List<List<Tile>> tiles)
    {
        //StartCoroutine(SolvePuzzleRandomOrder(startingNumbers, tiles));
        while (!SolvePuzzleRandomOrder(startingNumbers, tiles)) 
        {
            ResetGrid(tiles);
        }
    }

    public bool SolvePuzzleRandomOrder(int difficultyNumber, List<List<Tile>> tiles)
    {
        List<int> index = new List<int>();
        for (int i = 0; i < tiles.Count * tiles[0].Count; i++)
        {
            index.Add(i);
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



            if (indexX == -1 || numPossible == 0)
            {
                //Debug.Log("Not possible");
                return false;
            }

            tiles[indexY][indexX].SetRandomNumber();

            int currentNum = tiles[indexY][indexX].solutionNumber;

            for (int i = 0; i < 9; i++)
            {
                tiles[indexY][i].RemovePossibleNum(currentNum);

                tiles[i][indexX].RemovePossibleNum(currentNum);
            }

            int gridX = indexX / 3;
            int gridY = indexY / 3;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    tiles[gridY * 3 + y][gridX * 3 + x].RemovePossibleNum(currentNum);

                }
            }
        }
        return true;
    }
    


    public void ResetGrid(List<List<Tile>> tiles)
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