using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ----------------------------------------------------------------
public class GameController : MonoBehaviour
{
    public GameObject PuzzleGO;
    public GameObject PlayerGO;

    public GameObject UITextAvailableMoves;
    public GameObject UITextMessage;

    public GameObject UICanvasGameplay;
    public GameObject UICanvasMessage;
    public GameObject UICanvasGameover;

    public int MovesAvailable = 0;

    private Puzzle PuzzleController;
    private Player PlayerController;

    private List<MapInfo> Maps;

    public int CurrentMapIndex = 0;

    private EGameState GameState = EGameState.GamePlay;

    private Dictionary<ESin, int> SinsScore;

    private bool DoorOpened = false;

    private int StartGameMoves = 0;
    private int StartLevelMoves = 0;

    public delegate void del_onDoorOpened();
    public del_onDoorOpened Event_onDoorOpened;

    private Messages Msgs;

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

        SinsScore = new Dictionary<ESin, int>();

        SetAvailableMoves(MovesAvailable);
        StartGameMoves = MovesAvailable;

        InitPuzzle(CurrentMapIndex);

        Msgs = new Messages();
    }

    // ----------------------------------------------------------------
    private void Update()
    {
        if (KeyboardHandleGameState())
            return;

        if (GameState != EGameState.GamePlay)
            return;

        UpdateGamePlay();
    }

    // ----------------------------------------------------------------
    private void UpdateGamePlay()
    {
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

        PositionOnGrid newPos = new PositionOnGrid(PlayerController.Pos.X, PlayerController.Pos.Y);
        Vector2 destination = PuzzleController.GetDestination(currentMap, ref newPos, dir, DoorOpened);

        bool madeMove = newPos.X != PlayerController.Pos.X || newPos.Y != PlayerController.Pos.Y;

        ESin? newSin = currentMap.GetSin(newPos);

        // If not moving to a board pos, move without penalization
        if (newSin.HasValue == false)
        {
            PlayerController.Pos = newPos;
            PlayerController.MoveTo(destination);
            return;
        }

        // Else move to an unlocked board pos and increase sin score
        PlayerController.Pos = newPos;
        PlayerController.MoveTo(destination);

        if (madeMove)
            SetAvailableMoves(MovesAvailable - 1);

        if (MovesAvailable <= 0)
        {
            SetGameState(EGameState.GameOver);
            return;
        }
    }

    // ----------------------------------------------------------------
    private bool KeyboardHandleGameState()
    {
        bool isGameRestart = Input.GetKeyDown(KeyCode.P);
        bool isLevelRestart = Input.GetKeyDown(KeyCode.L);
        bool isContinue = Input.GetKeyDown(KeyCode.Space);

        if (isGameRestart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

            //SetAvailableMoves(StartGameMoves);
            //InitPuzzle(0);
            //SetGameState(EGameState.GamePlay);
            //return true;
        }
        if (isLevelRestart)
        {
            SetAvailableMoves(StartLevelMoves);
            InitPuzzle(CurrentMapIndex);
            SetGameState(EGameState.GamePlay);
            return true;
        }
        if (isContinue && GameState == EGameState.Message)
        {
            SetGameState(EGameState.GamePlay);
        }

        return false;
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
    private void InitPuzzle(int mapIndex)
    {
        if (mapIndex < 0 || mapIndex >= Maps.Count)
        {
            SetGameState(EGameState.GameOver);
            return;
        }

        SetGameState(EGameState.GamePlay);

        CurrentMapIndex = mapIndex;
        MapInfo currentMap = Maps[mapIndex];

        currentMap.DebugLog();

        PositionOnGrid playerPos = currentMap.PlayerStartingPosRand();

        Rigidbody2D playerRb = PlayerController.GetComponent<Rigidbody2D>();
        if (playerRb == null)
            return;

        float zeroX = playerRb.transform.position.x - playerPos.X * PuzzleController.TileSize;
        float zeroY = playerRb.transform.position.y - playerPos.Y * PuzzleController.TileSize;

        PuzzleController.ClearBoard();
        PuzzleController.Generate(currentMap, zeroX, zeroY);
        PlayerController.Init(playerPos);

        StartLevelMoves = MovesAvailable;
    }

    // ----------------------------------------------------------------
    public void PowerupPickedUp(int powerupIndex, int movesAdded)
    {
        SetGameState(EGameState.Message);

        SetAvailableMoves(MovesAvailable + movesAdded);
        if (Event_onDoorOpened != null)
        {
            Event_onDoorOpened();
        }
        DoorOpened = true;

        UITextMessage.GetComponent<Text>().text = Msgs.GetMessage(powerupIndex + 1);
    }

    // ----------------------------------------------------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(PlayerGO.transform.position, 0.3f);
    }
    
    // ----------------------------------------------------------------
    private void SetAvailableMoves(int avaliableMoves)
    {
        MovesAvailable = avaliableMoves;
        UITextAvailableMoves.GetComponent<Text>().text = "Available moves: " + avaliableMoves;
    }

    // ----------------------------------------------------------------
    private void SetGameState(EGameState gameState)
    {
        EGameState prevState = GameState;
        GameState = gameState;

        UICanvasGameplay.SetActive(GameState == EGameState.GamePlay);
        UICanvasMessage.SetActive(GameState == EGameState.Message);
        UICanvasGameover.SetActive(GameState == EGameState.GameOver);

        PlayerController.SetMoving(GameState == EGameState.GamePlay);
    }
}
