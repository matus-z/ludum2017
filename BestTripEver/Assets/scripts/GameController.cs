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

    private int CurrentMapIndex = 0;

    private bool GameOver = true;

    private bool DoorOpened = false;

    public List<ESin> UnlockedSins;

    public delegate void del_onDoorOpened();
    public del_onDoorOpened Event_onDoorOpened;

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

        UnlockedSins = new List<ESin>();

        // TODO Matus : init unlocked sins from where?
        UnlockedSins.Add(ESin.Lust);

        InitPuzzle(CurrentMapIndex);
    }

    // ----------------------------------------------------------------
    private void Update()
    {
        if (GameOver)
            return;

        if (PlayerController.Moving)
            return;

        MapInfo currentMap = Maps[CurrentMapIndex];
        if (currentMap.IsTileType(PlayerController.Pos, ETile.Out))
        {
            DoorOpened = false;
            InitPuzzle(CurrentMapIndex + 1);
            return;
        }

        EDirection dir = DirectionFromKeyboard();
        if (dir == EDirection.No)
            return;

        Vector2 destination = PuzzleController.GetDestination(currentMap, ref PlayerController.Pos, dir, DoorOpened);
        PlayerController.MoveTo(destination);
    }

    // ----------------------------------------------------------------
    private EDirection DirectionFromKeyboard()
    {
        bool isUp = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        bool isDown = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
        bool isLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        bool isRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

        bool isMove = isUp || isDown || isLeft || isRight;
        if (!isMove)
            return EDirection.No;

        if (isUp)
            return EDirection.Up;
        else if (isRight)
            return EDirection.Right;
        else if (isDown)
            return EDirection.Down;
        else if (isLeft)
            return EDirection.Left;

        return EDirection.No;
    }

    // ----------------------------------------------------------------
    private bool Ok()
    {
        return PuzzleController != null && PlayerController != null && Maps != null && Maps.Count > 0;
    }

    // ----------------------------------------------------------------
    private void InitPuzzle(int mapIndex)
    {
        if (mapIndex < 0 || mapIndex >= Maps.Count)
        {
            Debug.Log("Game Over.");
            GameOver = true;
            return;
        }

        GameOver = false;

        CurrentMapIndex = mapIndex;
        MapInfo currentMap = Maps[mapIndex];

        currentMap.DebugLog();

        ZeroX = PlayerController.transform.position.x;
        ZeroY = PlayerController.transform.position.y;

        PuzzleController.ClearBoard();
        PuzzleController.Generate(currentMap, ZeroX, ZeroY);
        PositionOnGrid playerPos = PuzzleController.PlayerStartingPosRand(currentMap);
        PlayerController.Init(playerPos, ZeroX, ZeroY, PuzzleController.TileSize, UnlockedSins);
    }

    public void PowerupPickedUp() {
        if (Event_onDoorOpened != null) {
            Event_onDoorOpened();
        }
        DoorOpened = true;
    }

    public void SinPickedUp(int sinIndex)
    {
        UnlockedSins.Add((ESin)sinIndex);
        if (Event_onDoorOpened != null)
        {
        Event_onDoorOpened();
        }
        DoorOpened = true;
    }

    // ----------------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(ZeroX, ZeroY, 0.0f), 0.3f);
    }
}
