using System.Collections;
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

    public EDirection InDir = EDirection.Down;
    public List<EndingPoint> EndingPoints;

    List<List<int>> puzzleMap = new List<List<int>> {
        new List<int>() {1, 0, 0, 0, 0, 1},
        new List<int>() {1, 0, 2, 2, 1, 1},
        new List<int>() {2, 0, 2, 2, 2, 1},
        new List<int>() {1, 0, 0, 1, 2, 1},
        new List<int>() {1, 1, 2, 1, 1, 1},
        new List<int>() {1, 1, 2, 0, 0, 0},
    };

    private float TileSize = 4.21f;

    // ----------------------------------------------------------------
    void Start()
    {
        EndingPoints = new List<EndingPoint>();
        EndingPoints.Add(new EndingPoint(0, 4, 0));
        EndingPoints.Add(new EndingPoint(1, 0, 0));

        GeneratePuzzle(PuzzleMapExtended());
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

        int maxY = puzzleMap.Max(obj => obj.Count);
        return maxY;
    }

    // ----------------------------------------------------------------
    private List<List<int>> PuzzleMapExtended()
    {
        List<List<int>> res = new List<List<int>>();

        int dimCols = DimCols();
        int dimRows = DimRows();

        for (int col = 0; col < dimCols + 2; col++)
        {
            res.Add(new List<int>());
            for (int row = 0; row < dimRows + 2; row++)
            {
                res[res.Count - 1].Add(-10);
            }
        }

        for (int col = 0; col < dimCols; col++)
        {
            for (int row = 0; row < dimRows; row++)
            {
                res[row + 1][col + 1] = puzzleMap[row][col];
            }
        }

        switch (InDir)
        {
            case EDirection.Up:
                for (int i = 0; i < dimCols; i++)
                    res[0][i + 1] = -1;
                break;
            case EDirection.Right:
                for (int i = 0; i < dimRows; i++)
                    res[i + 1][dimCols + 1] = -1;
                break;
            case EDirection.Down:
                for (int i = 0; i < dimCols; i++)
                    res[dimRows + 1][i + 1] = -1;
                break;
            case EDirection.Left:
                for (int i = 0; i < dimRows; i++)
                    res[i + 1][0] = -1;
                break;
        }

        foreach (EndingPoint ep in EndingPoints)
            res[ep.X][ep.Y] = -2;

        return res;
    }

    // ----------------------------------------------------------------
    void GeneratePuzzle(List<List<int>> map)
    {
        int dimCols = DimCols();
        int dimRows = DimRows();

        for (int col = 0; col < dimCols + 2; col++)
        {
            for (int row = 0; row < dimRows + 2; row++)
            {
                GameObject prefab = null;
                int prefabIndex = map[row][col];

                if (prefabIndex >= 0 && prefabIndex < TilePrefabs.Count)
                    prefab = TilePrefabs[prefabIndex];

                if (prefabIndex == -1)
                    prefab = TileBeginPrefab;

                if (prefabIndex == -2)
                    prefab = TileEndPrefab;

                if (prefab == null)
                    continue;

                GameObject tile = Instantiate(prefab, new Vector3(offsetX + (col + 1) * TileSize, offsetY + (row + 1) * -TileSize, 0), Quaternion.identity);
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

        return new Vector2(offsetX + x * 4.21f, offsetY + y * -4.21f);
    }
}
