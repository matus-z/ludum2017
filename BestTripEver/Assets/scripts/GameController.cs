﻿using System.Collections.Generic;
using System;
using UnityEngine;

// ----------------------------------------------------------------
public class GameController : MonoBehaviour
{
    public GameObject PuzzleGO;
    public GameObject PlayerGO;

    private Puzzle PuzzleController;
    private Player PlayerController;

    private List<MapInfo> Maps;

    private int CurrentMapIndex = 0;

    private bool GameOver = true;

    private Dictionary<ESin, int> SinsScore;
    
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

        SinsScore = new Dictionary<ESin, int>();

        // TODO Matus : init unlocked sins from where?
        UnlockSin(ESin.Lust);
        UnlockSin(ESin.Gluttony);
        //UnlockSin(ESin.Greed);
        //UnlockSin(ESin.Sloth);

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

        PositionOnGrid newPos = new PositionOnGrid(PlayerController.Pos.X, PlayerController.Pos.Y);
        Vector2 destination = PuzzleController.GetDestination(currentMap, ref newPos, dir, DoorOpened);

        ESin? newSin = currentMap.GetSin(newPos);

        // If not moving to a board pos, move without penalization
        if (newSin.HasValue == false)
        {
            PlayerController.Pos = newPos;
            PlayerController.MoveTo(destination);
            return;
        }

        // If trying to move to a locked board pos, return
        if (IsSinUnlocked(newSin.Value) == false)
            return;

        // Else move to an unlocked board pos and increase sin score
        PlayerController.Pos = newPos;
        PlayerController.MoveTo(destination);
        IncreaseSinsScore(newSin.Value);
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

        PositionOnGrid playerPos = currentMap.PlayerStartingPosRand();

        Rigidbody2D playerRb = PlayerController.GetComponent<Rigidbody2D>();
        if (playerRb == null)
            return;

        float zeroX = playerRb.transform.position.x - playerPos.X * PuzzleController.TileSize;
        float zeroY = playerRb.transform.position.y - playerPos.Y * PuzzleController.TileSize;

        PuzzleController.ClearBoard();
        PuzzleController.Generate(currentMap, zeroX, zeroY);
        PlayerController.Init(playerPos);
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
        Gizmos.DrawSphere(PlayerGO.transform.position, 0.3f);
    }

    // ----------------------------------------------------------------
    private void UnlockSin(ESin sin)
    {
        SinsScore.Add(sin, 0);
    }

    // ----------------------------------------------------------------
    private bool IsSinUnlocked(ESin sin)
    {
        return SinsScore.ContainsKey(sin);
    }

    // ----------------------------------------------------------------
    private void IncreaseSinsScore(ESin sin)
    {
        SinsScore[sin] += 1;
        DebugLogScore();
    }

    // ----------------------------------------------------------------
    private void DebugLogScore()
    {
        String str = "SinScore:";
        foreach (var sin in SinsScore)
            str += " " + sin.Key + " " + sin.Value;

        Debug.Log(str);
    }
}
