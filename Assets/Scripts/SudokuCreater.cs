using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SudokuCreater : MonoBehaviour
{
    [SerializeField] private Transform parentTransform;
    [SerializeField] private GameObject smallGridPrefab;
    [SerializeField] private GameObject tilePrefab;
    private List<List<Tile>> solutionTiles;
    private List<List<Tile>> tiles;

    private void Start()
    {
        solutionTiles = new List<List<Tile>>();
        for (int y = 0; y < 9; y++)
        {
            List<Tile> row = new List<Tile>();
            
            for (int x = 0; x < 9; x++)
            {
                Tile tile = new Tile();
                GameObject obj = Instantiate(tilePrefab, parentTransform);
                
                tile.textMesh = obj.GetComponentInChildren<TextMeshProUGUI>();
                tile.textMesh.gameObject.SetActive(false);
                row.Add(tile);
            }
            solutionTiles.Add(row);
        }

        while (!SolvePuzzleRandomOrder()) 
        {
            ResetGrid();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            while (!SolvePuzzleRandomOrder())
            {
                ResetGrid();
            }
        }
    }

    public IEnumerator SolvePuzzle()
    {
        for (int y = 0; y < solutionTiles.Count; y++)
        {
            for (int x = 0; x < solutionTiles[y].Count; x++)
            {
                yield return new WaitForSeconds(.2f);
                if (solutionTiles[y][x].number != -1)
                    continue;
                if (solutionTiles[y][x].GetPossibleNumber.Count > 1)
                    solutionTiles[y][x].SetRandomNumber();
                else
                    solutionTiles[y][x].SetNumber(solutionTiles[y][x].GetPossibleNumber[0]);

                int currentNum = solutionTiles[y][x].number;

                for (int i = 0; i < 9; i++)
                {
                    if (!solutionTiles[y][i].RemovePossibleNum(currentNum))
                        Debug.Log("Wrong number");
                }

                for (int i = 0; i < 9; i++)
                {
                    if (!solutionTiles[i][x].RemovePossibleNum(currentNum))
                        Debug.Log("Wrong number");
                }
            }
        }
    }

    public bool SolvePuzzleRandomOrder()
    {
        for (int j = 0; j < solutionTiles.Count * solutionTiles[0].Count; j++)
        {
            int indexX = -1;
            int indexY = -1;
            int numPossible = 10;
            for (int iy = 0; iy < solutionTiles.Count; iy++)
            {
                for (int ix = 0; ix < solutionTiles[iy].Count; ix++)
                {
                    if (solutionTiles[iy][ix].number != -1)
                        continue;

                    if (solutionTiles[iy][ix].GetPossibleNumber.Count < numPossible)
                    {
                        indexX = ix;
                        indexY = iy;
                        numPossible = solutionTiles[iy][ix].GetPossibleNumber.Count;
                    }

                }
            }
            

            if(indexX == -1|| numPossible == 0)
            {
                Debug.Log("Not possible");
                return false ;
            }

            solutionTiles[indexY][indexX].SetRandomNumber();

            int currentNum = solutionTiles[indexY][indexX].number;

            for (int i = 0; i < 9; i++)
            {
                solutionTiles[indexY][i].RemovePossibleNum(currentNum);

            }

            for (int i = 0; i < 9; i++)
            {
                solutionTiles[i][indexX].RemovePossibleNum(currentNum);
            }

            int gridX = indexX / 3;
            int gridY = indexY / 3;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    solutionTiles[gridY *3 +y][gridX *3 +x].RemovePossibleNum(currentNum);

                }
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
                solutionTiles[y][x].ResetTile();
            }
        }
    }
}

class Tile
{
    public int number = -1;
    List<int> possibleNum;
    public TextMeshProUGUI textMesh;

    public Tile()
    {
        possibleNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9};
    }

    public List<int> GetPossibleNumber => possibleNum;

    public bool RemovePossibleNum(int num)
    {
        if (number != -1 && num == number)
            return false;
        
        if (possibleNum.Count <= 0)
            return false;

        if (!possibleNum.Contains(num))
            return false;

        possibleNum.Remove(num);
        if(possibleNum.Count == 1)
        {
            return true;
        }
        return false;
        
    }

    public void SetNumber(int num)
    {
        number = num;
        textMesh.gameObject.SetActive(true);    
        textMesh.text = number.ToString();
    }

    public void SetRandomNumber()
    {
        int rand = Random.Range(0, possibleNum.Count);
        SetNumber(possibleNum[rand]);
    }
    
    public void SetPossibleNumber()
    {
        if (possibleNum.Count == 0 || possibleNum.Count > 1)
            return;
        SetNumber(possibleNum[0]);
    }

    public void ResetTile()
    {
        number = -1;
        possibleNum.Clear();
        possibleNum = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        textMesh.text = "";
        textMesh.gameObject.SetActive(false);
    }
}

public class VisualTile
{

}