﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ----------------------------------------------------------------
public class Puzzle : MonoBehaviour
{
    public List<GameObject> TilePrefabs;
    public GameObject TileBeginPrefab;
    public GameObject TileEndPrefab;

    private float OffsetX = 0.0f;
    private float OffsetY = 0.0f;
    
    // TODO Matus : out with this
    public float TileSize = 4.21f;

    // ----------------------------------------------------------------
    private void Start()
    {
    }

    // ----------------------------------------------------------------
    public PositionOnGrid PlayerStartingPosRand(MapInfo mi)
    {
        switch(mi.StartingPosition)
        {
            case EDirection.Up:
                return new PositionOnGrid(Random.Range(0, mi.DimCols()) + 1, mi.DimRowsExtended() - 1);
            case EDirection.Down:
                return new PositionOnGrid(Random.Range(0, mi.DimCols()) + 1, 0);
            case EDirection.Left:
                return new PositionOnGrid(0, Random.Range(0, mi.DimRows()) + 1);
            case EDirection.Right:
                return new PositionOnGrid(mi.DimColsExtended() - 1, Random.Range(0, mi.DimRows()) + 1);
        }

        return new PositionOnGrid(0, 0);
    }

    // ----------------------------------------------------------------
    public void Generate(MapInfo mi, float offsetX, float offsetY)
    {
        OffsetX = offsetX;
        OffsetY = offsetY;

        List<List<int>> map = mi.PuzzleMapExtended;
        int dCE = mi.DimColsExtended();
        int dRE = mi.DimRowsExtended();

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
                    new Vector3(OffsetX + col * TileSize, OffsetY + row * TileSize, 0),
                    Quaternion.identity);

                tile.transform.SetParent(transform);
            }
        }
    }

    //// ----------------------------------------------------------------
    //public Vector2 getDestination(ref int x, ref int y, EDirection d)
    //{
    //    var destX = x;
    //    var destY = y;
    //    switch (d)
    //    {
    //        case EDirection.Up:
    //            for (int i = y - 1; i >= 0; i--)
    //            {
    //                if (puzzleMap[y - 1][x] == puzzleMap[i][x])
    //                {
    //                    destY = i;
    //                }
    //                else
    //                {
    //                    break;
    //                }
    //            }
    //            break;
    //        case EDirection.Right:
    //            for (int i = x + 1; i < puzzleMap[y].Count; i++)
    //            {
    //                if (puzzleMap[y][x + 1] == puzzleMap[y][i])
    //                {
    //                    destX = i;
    //                }
    //                else
    //                {
    //                    break;
    //                }
    //            }
    //            break;
    //        case EDirection.Down:
    //            for (int i = y + 1; i < puzzleMap.Count; i++)
    //            {
    //                if (puzzleMap[y + 1][x] == puzzleMap[i][x])
    //                {
    //                    destY = i;
    //                }
    //                else
    //                {
    //                    break;
    //                }
    //            }
    //            break;
    //        case EDirection.Left:
    //            for (int i = x - 1; i >= 0; i--)
    //            {
    //                if (puzzleMap[y][x - 1] == puzzleMap[y][i])
    //                {
    //                    destX = i;
    //                }
    //                else
    //                {
    //                    break;
    //                }
    //            }
    //            break;
    //    }

    //    x = destX;
    //    y = destY;

    //    return new Vector2(offsetX + x * TileSize, offsetY - y * TileSize);
    //}
}
