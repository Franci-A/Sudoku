using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoFunction : MonoBehaviour
{
    private Queue<Action> actions;

    public void AddAction(Action newAction)
    {
        actions.Enqueue(newAction);
    }

    public Action Undo()
    {
        return actions.Dequeue();
    }
}

public struct Action
{
    public List<TileChanged> changes;
}

public struct TileChanged
{
    public int x;
    public int y;
    public int number;
}