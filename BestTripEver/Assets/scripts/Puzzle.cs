using System.Collections.Generic;
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

    // ----------------------------------------------------------------
    public void ClearBoard()
    {
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);
    }

    // ----------------------------------------------------------------
    private Vector2 GetDestinationFromTile(PositionOnGrid pos)
    {
        return new Vector2(OffsetX + pos.X * TileSize, OffsetY + pos.Y * TileSize);
    }

    // ----------------------------------------------------------------
    public Vector2 GetDestination(MapInfo mi, ref PositionOnGrid playerPos, EDirection d)
    {
        int x = playerPos.X;
        int y = playerPos.Y;

        int destX = playerPos.X;
        int destY = playerPos.Y;

        List<List<int>> map = mi.PuzzleMapExtended;

        PositionOnGrid nextPos = playerPos.NextPos(d);

        // Ca not move if undef or void
        if (false == mi.IsDefined(nextPos) || mi.IsTileType(nextPos, ETile.Void))
            return GetDestinationFromTile(playerPos);

        // Can not move from board back to in tile
        if (mi.IsTileType(playerPos, ETile.In) == false && mi.IsTileType(nextPos, ETile.In))
            return GetDestinationFromTile(playerPos);

        // If in or out, move just one tile
        if (mi.IsTileType(nextPos, ETile.In) || mi.IsTileType(nextPos, ETile.Out))
        {
            playerPos = nextPos;
            return GetDestinationFromTile(playerPos);
        }

        // Else inside map - move through all tiles with the same color
        switch (d)
        {
            case EDirection.Up:
                for (int i = y + 1; i < map.Count; i++)
                {
                    if (map[y + 1][x] == map[i][x])
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
                for (int i = x + 1; i < map[y].Count; i++)
                {
                    if (map[y][x + 1] == map[y][i])
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
                for (int i = y - 1; i >= 0; i--)
                {
                    if (map[y - 1][x] == map[i][x])
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
                    if (map[y][x - 1] == map[y][i])
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

        playerPos.X = destX;
        playerPos.Y = destY;

        return GetDestinationFromTile(playerPos);
    }
}
