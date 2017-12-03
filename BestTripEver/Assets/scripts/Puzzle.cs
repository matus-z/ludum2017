﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public List<GameObject> TilePrefabs;
    public GameObject TileBeginPrefab;
    public GameObject TileEndPrefab;

    public float offsetX = 0.0f;
    public float offsetY = 0.0f;

    //public EDirection InPos = EDirection.Down;
    //public List<EndingPoint> EndingPoints;

    //List<List<int>> puzzleMap = new List<List<int>> {
    //    new List<int>() {1, 0, 0, 0, 0, 1},
    //    new List<int>() {1, 0, 2, 2, 1, 1},
    //    new List<int>() {2, 0, 2, 2, 2, 1},
    //    new List<int>() {1, 0, 0, 1, 2, 1},
    //    new List<int>() {1, 1, 2, 1, 1, 1},
    //    new List<int>() {1, 1, 2, 0, 0, 0},
    //};

    private float TileSize = 4.21f;

    // ----------------------------------------------------------------
    private void Start()
    {
        EndingPoints = new List<EndingPoint>();
        EndingPoints.Add(new EndingPoint(0, 4));
        EndingPoints.Add(new EndingPoint(1, 0));
    }

    // ----------------------------------------------------------------
    private int DimCols()
    {
        return puzzleMap.Count;
    }

    // ----------------------------------------------------------------
    private int DimRows()
    {
        if (puzzleMap.Count <= 0)
            return 0;

        return puzzleMap.Max(obj => obj.Count);
    }

    private int DimColsExtended() { return DimCols() + 2; }
    private int DimRowsExtended() { return DimRows() + 2; }

    // ----------------------------------------------------------------
    private int PlayerStartingPosRand()
    {
        return Random.Range(0, InPos == EDirection.Up || InPos == EDirection.Down ? DimRows() : DimCols());
    }

    // ----------------------------------------------------------------
    private List<List<int>> PuzzleMapExtended(MapInfo mi)
    {
        List<List<int>> res = new List<List<int>>();

        int dC = DimCols();
        int dR = DimRows();
        int dCE = DimColsExtended();
        int dRE = DimRowsExtended();

        // Fill extended map with empty tiles
        for (int col = 0; col < dCE; col++)
        {
            res.Add(new List<int>());
            for (int row = 0; row < dRE; row++)
            {
                res[res.Count - 1].Add((int)ETile.Void);
            }
        }

        // Add defined unextended tiles
        for (int col = 0; col < dC; col++)
        {
            for (int row = 0; row < dR; row++)
            {
                res[row + 1][col + 1] = mi.puzzleMap[row][col];
            }
        }

        // Add tiles for in direction
        switch (mi.InPos)
        {
            case EDirection.Up:
                for (int i = 0; i < dC; i++)
                    res[0][i + 1] = (int)ETile.In;
                break;
            case EDirection.Right:
                for (int i = 0; i < dR; i++)
                    res[i + 1][dC + 1] = (int)ETile.In;
                break;
            case EDirection.Down:
                for (int i = 0; i < dC; i++)
                    res[dR + 1][i + 1] = (int)ETile.In;
                break;
            case EDirection.Left:
                for (int i = 0; i < dR; i++)
                    res[i + 1][0] = (int)ETile.In;
                break;
        }

        // Add tiles for in out directions
        foreach (EndingPoint ep in mi.EndingPoints)
            res[ep.X][ep.Y] = (int)ETile.Out;

        return res;
    }

    // ----------------------------------------------------------------
    public void Generate(MapInf mi)
    {
        List<List<int>> puzzleMap = PuzzleMapExtended(mi);

        GenerateExended(puzzleMap);
    }

    // ----------------------------------------------------------------
    private void GenerateExended(List<List<int>> map)
    {
        int dCE = DimColsExtended();
        int dRE = DimRowsExtended();

        for (int col = 0; col < dCE; col++)
        {
            for (int row = 0; row < dRE; row++)
            {
                GameObject prefab = null;
                int prefabIndex = map[row][col];

                if (prefabIndex >= 0 && prefabIndex < TilePrefabs.Count)
                    prefab = TilePrefabs[prefabIndex];

                if (prefabIndex == (int)ETile.In)
                    prefab = TileBeginPrefab;

                if (prefabIndex == (int)ETile.Out)
                    prefab = TileEndPrefab;

                if (prefab == null)
                    continue;

                GameObject tile = Instantiate(
                    prefab,
                    new Vector3(offsetX + (col + 1) * TileSize, offsetY - (row + 1) * TileSize, 0),
                    Quaternion.identity);

                tile.transform.SetParent(transform);
            }
        }
    }

    // ----------------------------------------------------------------
    public Vector2 getDestination(ref int x, ref int y, EDirection d)
    {
        var destX = x;
        var destY = y;
        switch (d)
        {
            case EDirection.Up:
                for (int i = y - 1; i >= 0; i--)
                {
                    if (puzzleMap[y - 1][x] == puzzleMap[i][x])
                    {
                        destY = i;
                    }
                    else
                    {
                        break;
                    }
                }
                break;
            case EDirection.Right:
                for (int i = x + 1; i < puzzleMap[y].Count; i++)
                {
                    if (puzzleMap[y][x + 1] == puzzleMap[y][i])
                    {
                        destX = i;
                    }
                    else
                    {
                        break;
                    }
                }
                break;
            case EDirection.Down:
                for (int i = y + 1; i < puzzleMap.Count; i++)
                {
                    if (puzzleMap[y + 1][x] == puzzleMap[i][x])
                    {
                        destY = i;
                    }
                    else
                    {
                        break;
                    }
                }
                break;
            case EDirection.Left:
                for (int i = x - 1; i >= 0; i--)
                {
                    if (puzzleMap[y][x - 1] == puzzleMap[y][i])
                    {
                        destX = i;
                    }
                    else
                    {
                        break;
                    }
                }
                break;
        }

        x = destX;
        y = destY;

        return new Vector2(offsetX + x * TileSize, offsetY - y * TileSize);
    }
}
