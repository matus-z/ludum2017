using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Sprite openedSprite;
    private GameController gc;

    // ----------------------------------------------------------------
    void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gc.Event_onDoorOpened += OpenDoor;
    }

    // ----------------------------------------------------------------
    void OpenDoor()
    {
        GetComponent<SpriteRenderer>().sprite = openedSprite;
        gc.Event_onDoorOpened -= OpenDoor;
    }

    // ----------------------------------------------------------------
    void OnDestroy()
    {
        gc.Event_onDoorOpened -= OpenDoor;
    }
}
