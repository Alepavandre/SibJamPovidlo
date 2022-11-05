using UnityEngine;

public class CheckerTrigger : MonoBehaviour
{
    public bool checkFurnit = false;
    public bool checkWalls = false;
    public static float step = 2f;

    private void Awake()
    {
        checkFurnit = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Walls"))
        {
            checkWalls = true;
            Debug.Log($"Entered walls {checkWalls}");
        }

        if (other.CompareTag("Furnit"))
        {
            checkFurnit = true;
            Debug.Log($"Entered furnit {checkFurnit}");
        }
    }

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
            Debug.Log($"Furnit {checkFurnit}");
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
            Debug.Log($"FurnitExit {checkFurnit}");
        }
    }
}