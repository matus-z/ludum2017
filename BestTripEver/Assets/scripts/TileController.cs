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
        if (sinIndex > 0 && gc.SinsScore.ContainsKey((ESin)sinIndex))
        {
            unlockingSin = true;
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !touched)
        {
            if (unlockingSin)
            {
                // TOOD: show UI with text using sinIndex
                gc.SinPickedUp(sinIndex);
            }
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            touched = true;
        }
    }
}
