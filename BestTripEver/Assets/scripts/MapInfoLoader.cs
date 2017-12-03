using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

// ----------------------------------------------------------------
public class MapInfoLoader
{
    // ----------------------------------------------------------------
    public List<MapInfo> LoadFromFile()
    {
        List<MapInfo> result = new List<MapInfo>();
        TextAsset textFile = Resources.Load("maps") as TextAsset;
        var lines = textFile.text.Split(new char[] { '\n' });
        List<List<int>> puzzleMap = new List<List<int>>();
        List<EndingPoint> endingPoints = new List<EndingPoint>();
        List<SinPoint> sinPoints = new List<SinPoint>();
        List<PowerupPoint> powerupPoints = new List<PowerupPoint>();
        EDirection startingPosition = EDirection.Down;
        string[] values;

        foreach (var lineIter in lines)
        {
            String line = lineIter.Trim();
            if (line.Length <= 0)
            {
                result.Add(new MapInfo(puzzleMap, endingPoints, sinPoints, powerupPoints, startingPosition));

                puzzleMap = new List<List<int>>();
                endingPoints = new List<EndingPoint>();
                sinPoints = new List<SinPoint>();
                powerupPoints = new List<PowerupPoint>();

                continue;
            }

            switch (line[0])
            {
                case 'e':
                    values = line.Split(new char[] { ' ' });
                    endingPoints.Add(new EndingPoint(int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3])));
                    break;
                case 's':
                    values = line.Split(new char[] { ' ' });
                    sinPoints.Add(new SinPoint(int.Parse(values[1]), int.Parse(values[2]), (ESin)int.Parse(values[3])));
                    break;
                case 'p':
                    values = line.Split(new char[] { ' ' });
                    powerupPoints.Add(new PowerupPoint(int.Parse(values[1]), int.Parse(values[2])));
                    break;
                case '[':
                    values = line.Split(new char[] { '[', ']', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var tmp = new List<int>();
                    for (int i = 0; i < values.Length; i++)
                    {
                        var x = values[i].Trim();
                        if (x.Length > 0)
                        {
                            tmp.Add(int.Parse(x));
                        }
                    }
                    puzzleMap.Add(tmp);
                    break;
                default:
                    if (line.Trim().Length > 0)
                    {
                        startingPosition = (EDirection)int.Parse(line.Trim());
                    }
                    break;
            }
        }

        return result;
    }
}
