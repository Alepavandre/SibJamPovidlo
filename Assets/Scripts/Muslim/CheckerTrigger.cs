using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerTrigger : MonoBehaviour
{
    public bool checkFurnit = false;
    public bool checkWalls = false;
    public static float step = 0.5f;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Walls"))
        {
            checkWalls = true;
        }

        if (other.CompareTag("Furnit"))
        {
            checkFurnit = true;
        }
    }*/

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Walls"))
        {
            checkWalls = true;
            Debug.Log($"Bool walls {checkWalls}");
        }

        if (other.CompareTag("Furnit"))
        {
            checkFurnit = true;
            Debug.Log($"Furnit walls {checkFurnit}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Walls"))
        {
            checkWalls = false;
            Debug.Log($"BoolExit walls {checkWalls}");
        }

        if (other.CompareTag("Furnit"))
        {
            checkFurnit = false;
            Debug.Log($"FurnitExit walls {checkFurnit}");
        }
    }
}