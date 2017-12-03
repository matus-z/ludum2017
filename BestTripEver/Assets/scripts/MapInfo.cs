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
  public MapInfo(List<List<int>> puzzleMap, List<EndingPoint> endingPoints, List<SinPoint> sinPoints,List<PowerupPoint> powerupPoints, EDirection startingPosition)
  {
    PuzzleMap = puzzleMap;
    EndingPoints = endingPoints;
    SinPoints = sinPoints;
    PowerupPoints = powerupPoints;
    StartingPosition = startingPosition;
  }
}