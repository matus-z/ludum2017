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

    private MapInfo CurrentMap;

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
        if (Maps.Count <= 0)
            return;

        InitPuzzle(Maps[0]);
    }

    // ----------------------------------------------------------------
    private void Update()
    {
        if (PlayerController.Moving)
            return;

        bool isUp = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        bool isDown = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
        bool isLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        bool isRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

        bool isMove = isUp || isDown || isLeft || isRight;
        if (!isMove)
            return;

        EDirection dir = EDirection.No;
        if (isUp)
            dir = EDirection.Up;
        else if (isRight)
            dir = EDirection.Right;
        else if (isDown)
            dir = EDirection.Down;
        else if (isLeft)
            dir = EDirection.Left;

        Vector2 destination = PuzzleController.GetDestination(CurrentMap, ref PlayerController.Pos, dir);
        PlayerController.MoveTo(destination);
    }

    // ----------------------------------------------------------------
    private bool Ok()
    {
        return PuzzleController != null && PlayerController != null && Maps != null && Maps.Count > 0;
    }

    // ----------------------------------------------------------------
    private void InitPuzzle(MapInfo mi)
    {
        CurrentMap = mi;

        PuzzleController.Generate(mi, ZeroX, ZeroY);
        PositionOnGrid playerPos = PuzzleController.PlayerStartingPosRand(mi);
        PlayerController.Init(playerPos, ZeroX, ZeroY, PuzzleController.TileSize);
    }

    // ----------------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(ZeroX, ZeroY, 0.0f), 0.3f);
    }
}
