using System;
using UnityEngine;

// ----------------------------------------------------------------
public class PositionOnGrid
{
    public int X = 0;
    public int Y = 0;

    // ----------------------------------------------------------------
    public PositionOnGrid(int x, int y)
    {
        X = x;
        Y = y;
    }

    // ----------------------------------------------------------------
    public PositionOnGrid NextPos(EDirection d)
    {
        switch (d)
        {
            case EDirection.Up:
                return new PositionOnGrid(X, Y + 1);
            case EDirection.Right:
                return new PositionOnGrid(X + 1, Y);
            case EDirection.Down:
                return new PositionOnGrid(X, Y - 1);
            case EDirection.Left:
                return new PositionOnGrid(X - 1, Y);
        }

        Debug.Log("Should not get here!");
        return this;
    }
}
