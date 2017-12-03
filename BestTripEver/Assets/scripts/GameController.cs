using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
public class GameController : MonoBehaviour
{
    public GameObject PuzzleGO;
    public GameObject PlayerGO;

    private Puzzle PuzzleController;
    private Player PlayerController;

    private float ZeroX = 0;
    private float ZeroY = 0;

    private List<MapInfo> Maps;

    // ----------------------------------------------------------------
    private void Start()
    {
        if (PuzzleGO == null || PlayerGO == null)
            return;

        Puzzle pz = PuzzleGO.GetComponent<Puzzle>();
        Player pl = PlayerGO.GetComponent<Player>();

        if (pz == null || pl == null)
            return;

        PuzzleController = pz;
        PlayerController = pl;

        MapInfoLoader mapsLoader = new MapInfoLoader();
        Maps = mapsLoader.LoadFromFile();

        InitPuzzle(Maps[3]);
    }

    // ----------------------------------------------------------------
    private bool Ok()
    {
        return PuzzleController != null && PlayerController != null && Maps != null && Maps.Count > 0;
    }

    // ----------------------------------------------------------------
    private void InitPuzzle(MapInfo mi)
    {
        PuzzleController.Generate(mi, ZeroX, ZeroY);
        PositionOnGrid playerPos = PuzzleController.PlayerStartingPosRand(mi);

        Debug.Log(playerPos.X + " " + playerPos.Y);

        PlayerController.Init(playerPos, ZeroX, ZeroY, PuzzleController.TileSize);
    }

    // ----------------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(ZeroX, ZeroY, 0.0f), 0.3f);
    }
}
