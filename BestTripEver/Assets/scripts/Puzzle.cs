using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
public class Puzzle : MonoBehaviour
{
    public List<GameObject> TilePrefabs;
    public List<GameObject> PowerupPrefabs;
    public List<GameObject> WallPrefabs;
    public List<GameObject> CornerPrefabs;
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

        GameObject tile;
        List<List<int>> map = mi.PuzzleMapExtended;
        int dCE = mi.DimColsExtended();
        int dRE = mi.DimRowsExtended();

        for (int col = 0; col < dCE; col++)
        {
            for (int row = 0; row < dRE; row++)
            {
                GameObject prefab = null;
                int prefabIndex = map[row][col];
                int rotation = 0;

                if (prefabIndex >= 0 && prefabIndex < TilePrefabs.Count)
                    prefab = TilePrefabs[prefabIndex];

                if (prefabIndex == (int)ETile.In)
                    prefab = TileBeginPrefab;

                if (prefabIndex == (int)ETile.Out) {
                    prefab = TileEndPrefab;
                    if (row == dRE - 1) {
                        rotation = 0;
                    } else if (col == 0) {
                        rotation = 90;
                    } else if (row == 0) {
                        rotation = 180;
                    } else if (col == dCE - 1) {
                        rotation = 270;
                    }
                }

                if (prefab == null)
                    continue;

                tile = Instantiate(
                    prefab,
                    new Vector3(OffsetX + col * TileSize, OffsetY + row * TileSize, 0),
                    Quaternion.Euler(new Vector3(0, 0, rotation)));
                if (prefabIndex >= 0 && prefabIndex < TilePrefabs.Count)
                    tile.GetComponent<TileController>().sinIndex = prefabIndex;
                tile.transform.SetParent(transform);
            }
        }

        foreach (var powerupPoint in mi.PowerupPoints) {
            tile = Instantiate(
                PowerupPrefabs[powerupPoint.PowerupIndex],
                new Vector3(OffsetX + powerupPoint.X * TileSize, OffsetY + powerupPoint.Y * TileSize, 0),
                Quaternion.identity);
            tile.GetComponent<PowerupController>().powerupIndex = powerupPoint.PowerupIndex;
            tile.transform.SetParent(transform);
        }

        int wallPrefabIndex = Random.Range(0, WallPrefabs.Count);
        switch (mi.StartingPosition)
        {
        case EDirection.Up:
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX + (dCE - 1) * TileSize, OffsetY + dRE * TileSize, 0),
                Quaternion.Euler(new Vector3(0, 0, 270)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX + (dCE - 1) * TileSize, OffsetY, 0),
                Quaternion.Euler(new Vector3(0, 0, 180)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX, OffsetY, 0),
                Quaternion.Euler(new Vector3(0, 0, 90)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX, OffsetY + dRE * TileSize, 0),
                Quaternion.Euler(new Vector3(0, 0, 0)));
            tile.transform.SetParent(transform);

            for (int i = 1; i < dCE - 1; i++) {
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + i * TileSize, OffsetY, 0),
                    Quaternion.Euler(new Vector3(0, 0, 180)));
                tile.transform.SetParent(transform);
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + i * TileSize, OffsetY + dRE * TileSize, 0),
                    Quaternion.identity);
                tile.transform.SetParent(transform);
            }
            for (int i = 0; i < dRE; i++)
            {
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX, OffsetY + i * TileSize, 0),
                    Quaternion.Euler(new Vector3(0, 0, 90)));
                tile.transform.SetParent(transform);
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + (dCE - 1) * TileSize, OffsetY + i * TileSize, 0),
                    Quaternion.Euler(new Vector3(0, 0, 270)));
                tile.transform.SetParent(transform);
            }
            break;
        case EDirection.Down:
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX + (dCE - 1) * TileSize, OffsetY + (dRE - 1) * TileSize, 0),
                Quaternion.Euler(new Vector3(0, 0, 270)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX + (dCE - 1) * TileSize, OffsetY - TileSize, 0),
                Quaternion.Euler(new Vector3(0, 0, 180)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX, OffsetY - TileSize, 0),
                Quaternion.Euler(new Vector3(0, 0, 90)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX, OffsetY + (dRE - 1) * TileSize, 0),
                Quaternion.Euler(new Vector3(0, 0, 0)));
            tile.transform.SetParent(transform);

            for (int i = 1; i < dCE - 1; i++)
            {
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + i * TileSize, OffsetY - TileSize, 0),
                    Quaternion.Euler(new Vector3(0, 0, 180)));
                tile.transform.SetParent(transform);
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + i * TileSize, OffsetY + (dRE - 1) * TileSize, 0),
                    Quaternion.identity);
                tile.transform.SetParent(transform);
            }
            for (int i = 0; i < dRE - 1; i++)
            {
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX, OffsetY + i * TileSize, 0),
                    Quaternion.Euler(new Vector3(0, 0, 90)));
                tile.transform.SetParent(transform);
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + (dCE - 1) * TileSize, OffsetY + i * TileSize, 0),
                    Quaternion.Euler(new Vector3(0, 0, 270)));
                tile.transform.SetParent(transform);
            }
            break;
        case EDirection.Left:
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX + (dCE - 1) * TileSize, OffsetY + (dRE - 1) * TileSize, 0),
                Quaternion.Euler(new Vector3(0, 0, 270)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX + (dCE - 1) * TileSize, OffsetY, 0),
                Quaternion.Euler(new Vector3(0, 0, 180)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX - TileSize, OffsetY, 0),
                Quaternion.Euler(new Vector3(0, 0, 90)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX - TileSize, OffsetY + (dRE - 1) * TileSize, 0),
                Quaternion.Euler(new Vector3(0, 0, 0)));
            tile.transform.SetParent(transform);
            for (int i = 0; i < dCE - 1; i++)
            {
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + i * TileSize, OffsetY, 0),
                    Quaternion.Euler(new Vector3(0, 0, 180)));
                tile.transform.SetParent(transform);
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + i * TileSize, OffsetY + (dRE - 1) * TileSize, 0),
                    Quaternion.identity);
                tile.transform.SetParent(transform);
            }
            for (int i = 1; i < dRE - 1; i++)
            {
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX - TileSize, OffsetY + i * TileSize, 0),
                    Quaternion.Euler(new Vector3(0, 0, 90)));
                tile.transform.SetParent(transform);
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + (dCE - 1) * TileSize, OffsetY + i * TileSize, 0),
                    Quaternion.Euler(new Vector3(0, 0, 270)));
                tile.transform.SetParent(transform);
            }
            break;
        case EDirection.Right:
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX + dCE * TileSize, OffsetY + (dRE - 1) * TileSize, 0),
                Quaternion.Euler(new Vector3(0, 0, 270)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX + dCE * TileSize, OffsetY, 0),
                Quaternion.Euler(new Vector3(0, 0, 180)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX, OffsetY, 0),
                Quaternion.Euler(new Vector3(0, 0, 90)));
            tile.transform.SetParent(transform);
            tile = Instantiate(
                CornerPrefabs[wallPrefabIndex],
                new Vector3(OffsetX, OffsetY + (dRE - 1) * TileSize, 0),
                Quaternion.Euler(new Vector3(0, 0, 0)));
            tile.transform.SetParent(transform);
            for (int i = 0; i < dCE; i++)
            {
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + i * TileSize, OffsetY, 0),
                    Quaternion.Euler(new Vector3(0, 0, 180)));
                tile.transform.SetParent(transform);
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + i * TileSize, OffsetY + (dRE - 1) * TileSize, 0),
                    Quaternion.identity);
                tile.transform.SetParent(transform);
            }
            for (int i = 1; i < dRE - 1; i++)
            {
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX, OffsetY + i * TileSize, 0),
                    Quaternion.Euler(new Vector3(0, 0, 90)));
                tile.transform.SetParent(transform);
                tile = Instantiate(
                    WallPrefabs[wallPrefabIndex],
                    new Vector3(OffsetX + dCE * TileSize, OffsetY + i * TileSize, 0),
                    Quaternion.Euler(new Vector3(0, 0, 270)));
                tile.transform.SetParent(transform);
            }
            break;
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
    public Vector2 GetDestination(MapInfo mi, ref PositionOnGrid playerPos, EDirection d, bool doorOpened)
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

        // If in, move just one tile
        if (mi.IsTileType(nextPos, ETile.In))
        {
            playerPos = nextPos;
            return GetDestinationFromTile(playerPos);
        }

        // If out, move only if door opened
        if (mi.IsTileType(nextPos, ETile.Out))
        {
            if (doorOpened) {
                playerPos = nextPos;
            }
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
