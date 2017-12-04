using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public int powerupIndex = 0;
    public int MovesAdded = 3;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // TODO: Show UI with text using powerupIndex
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PowerupPickedUp(MovesAdded);
            Destroy(gameObject);
        }
    }
}
