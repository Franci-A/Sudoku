using System.Collections.Generic;
using UnityEngine;

public class SquareHandler : MonoBehaviour
{
    [SerializeField] private List<TileHandler> tileHandlers = new List<TileHandler>();

    public TileHandler GetTile(int index)
    {
        return tileHandlers[index];
    }
}
