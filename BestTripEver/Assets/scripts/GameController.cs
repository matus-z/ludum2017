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

        // ...
        if(Ok())
            InitSamplePuzzle();
    }

    // ----------------------------------------------------------------
    private bool Ok()
    {
        return PuzzleController != null && PlayerController != null;
    }

    // ----------------------------------------------------------------
    private void InitSamplePuzzle()
    {
        // TODO Matus : place somewhere else
        List<List<int>> puzzleMap = new List<List<int>> {
        new List<int>() {1, 0, 0, 0, 0, 1},
        new List<int>() {1, 0, 2, 2, 1, 1},
        new List<int>() {2, 0, 2, 2, 2, 1},
        new List<int>() {1, 0, 0, 1, 2, 1},
        new List<int>() {1, 1, 2, 1, 1, 1},
        new List<int>() {1, 1, 2, 0, 0, 0},
        };

        List<EndingPoint> endingPoints;

        endingPoints = new List<EndingPoint>();

        endingPoints.Add(new EndingPoint(0, 4, 0));
        endingPoints.Add(new EndingPoint(1, 0, 0));

        List<SinPoint> sinPoints = new List<SinPoint>();
        List<PowerupPoint> powerupPoints = new List<PowerupPoint>();

        MapInfo mi = new MapInfo(
            puzzleMap, 
            endingPoints,
            sinPoints,
            powerupPoints,
            EDirection.Down);

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
