using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ----------------------------------------------------------------
public class MapInfo
{
    public List<List<int>> PuzzleMap;
    public List<EndingPoint> EndingPoints;
    public List<SinPoint> SinPoints;
    public List<PowerupPoint> PowerupPoints;
    public EDirection StartingPosition;
    public List<List<int>> PuzzleMapExtended;

    // ----------------------------------------------------------------
    public MapInfo(List<List<int>> puzzleMap, List<EndingPoint> endingPoints, List<SinPoint> sinPoints, List<PowerupPoint> powerupPoints, EDirection startingPosition)
    {
        PuzzleMap = puzzleMap;
        EndingPoints = endingPoints;
        SinPoints = sinPoints;
        PowerupPoints = powerupPoints;
        StartingPosition = startingPosition;

        PuzzleMapExtended = ComputePuzzleMapExtended();
    }

    // ----------------------------------------------------------------
    public int DimRows()
    {
        return PuzzleMap.Count;
    }

    // ----------------------------------------------------------------
    public int DimCols()
    {
        if (PuzzleMap.Count <= 0)
            return 0;

        return PuzzleMap.Max(obj => obj.Count);
    }

    // ----------------------------------------------------------------
    public int DimRowsExtended() { return DimRows() + 2; }
    public int DimColsExtended() { return DimCols() + 2; }

    // ----------------------------------------------------------------
    private List<List<int>> ComputePuzzleMapExtended()
    {
        List<List<int>> res = new List<List<int>>();

        int dR = DimRows();
        int dC = DimCols();
        int dRE = DimRowsExtended();
        int dCE = DimColsExtended();

        // Fill extended map with empty tiles
        for (int row = 0; row < dRE; row++)
        {
            res.Add(new List<int>());
            for (int col = 0; col < dCE; col++)
            {
                res[res.Count - 1].Add((int)ETile.Void);
            }
        }

        // Add defined unextended tiles
        for (int row = 0; row < dR; row++)
        {
            for (int col = 0; col < dC; col++)
            {
                res[row + 1][col + 1] = PuzzleMap[row][col];
            }
        }

        // Add tiles for starting direction
        switch (StartingPosition)
        {
            case EDirection.Up:
                for (int i = 0; i < dC; i++)
                    res[dR + 1][i + 1] = (int)ETile.In;
                break;
            case EDirection.Down:
                for (int i = 0; i < dC; i++)
                    res[0][i + 1] = (int)ETile.In;
                break;
            case EDirection.Left:
                for (int i = 0; i < dR; i++)
                    res[i + 1][0] = (int)ETile.In;
                break;
            case EDirection.Right:
                for (int i = 0; i < dR; i++)
                    res[i + 1][dC + 1] = (int)ETile.In;
                break;
        }

        // Add tiles for out directions
        foreach (EndingPoint ep in EndingPoints)
            res[ep.Pos.Y][ep.Pos.X] = (int)ETile.Out;

        return res;
    }

    // ----------------------------------------------------------------
    public bool IsDefined(PositionOnGrid p)
    {
        return p.X >= 0 && p.Y >= 0 && p.X < DimColsExtended() && p.Y < DimRowsExtended();
    }

    // ----------------------------------------------------------------
    public bool IsTileType(PositionOnGrid p, ETile type)
    {
        return (int)type == PuzzleMapExtended[p.Y][p.X];
    }

    // ----------------------------------------------------------------
    public void DebugLog()
    {
        Debug.Log("Map: [r,x]=[" + DimRows() + "," + DimCols() + "] Ends[" + EndingPoints.Count + "] Poweups[" + PowerupPoints.Count + "] Sins[" + SinPoints.Count + "] Start[" + StartingPosition + "]");
    }
}
