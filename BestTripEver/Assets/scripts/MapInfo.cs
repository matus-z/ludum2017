using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ----------------------------------------------------------------
public class MapInfo
{
    public List<List<int>> PuzzleMap;
    public List<EndingPoint> EndingPoints;
    public List<SinPoint> SinPoints;
    public List<PowerupPoint> PowerupPoints;
    public EDirection StartingPosition;

    // ----------------------------------------------------------------
    public MapInfo(List<List<int>> puzzleMap, List<EndingPoint> endingPoints, List<SinPoint> sinPoints, List<PowerupPoint> powerupPoints, EDirection startingPosition)
    {
        PuzzleMap = puzzleMap;
        EndingPoints = endingPoints;
        SinPoints = sinPoints;
        PowerupPoints = powerupPoints;
        StartingPosition = startingPosition;
    }

    // ----------------------------------------------------------------
    public int DimCols()
    {
        return PuzzleMap.Count;
    }

    // ----------------------------------------------------------------
    public int DimRows()
    {
        if (PuzzleMap.Count <= 0)
            return 0;

        return PuzzleMap.Max(obj => obj.Count);
    }

    // ----------------------------------------------------------------
    public int DimColsExtended() { return DimCols() + 2; }
    public int DimRowsExtended() { return DimRows() + 2; }
}
