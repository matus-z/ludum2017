using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private bool touched = false;
    public bool unlockingSin = false;
    public int sinIndex = 0;

    private GameController gc;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !touched)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            touched = true;
        }
    }
}
