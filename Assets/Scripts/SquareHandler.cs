using System.Collections.Generic;
using UnityEngine;

public class SquareHandler : MonoBehaviour
{
    [SerializeField] private List<TileHandler> tileHandlers = new List<TileHandler>();

    public TileHandler GetTile(int index)
    {
        return tileHandlers[index];
    }

    public bool HasNumber(int number)
    {
        for (int i = 0; i < tileHandlers.Count; i++)
        {
            if(tileHandlers[i].tile.placedNumber == number)
                return true;
        }
        return false;
    }
}
