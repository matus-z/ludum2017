using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private bool touched = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !touched)
        {
            GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color * 2;
            touched = true;
        }
    }
}
