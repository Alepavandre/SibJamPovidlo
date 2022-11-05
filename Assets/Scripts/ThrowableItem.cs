using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItem : MonoBehaviour
{
    [SerializeField]
    private float distance = 13f;
    [SerializeField]
    private float speedItem = 0.5f;
    private ItemPickUp item;
    [SerializeField] private GameObject checker;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        item = ItemSystem.ItemInstance.minItem;
        item.transform.position = transform.position;
        item.transform.SetParent(transform);
        ItemSystem.ItemInstance.isPicked = false;
        ItemSystem.ItemInstance.isEntered = false;
        if (item != null)
        {
            item.enabled = false;
        }
        else
        {
            Debug.Log("Where is my child?!");
            return;
        }
        StartCoroutine(nameof(Throw));
    }

    IEnumerator Throw()
    {
        //Vector2 startPos = rb.position;
        //Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 endPos = startPos + (clickPos - startPos).normalized * distance;
        Vector2 endPos = EndPoseDis();
        for (float i = 0f; i < distance; i += speedItem)
        {
            yield return new WaitForSeconds(.01f);
            rb.position = Vector2.MoveTowards(rb.position, endPos, speedItem);
        }
        EndMission();
    }

    private Vector2 EndPoseDis()
    {
        Vector2 startPos = rb.position;
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 endPos = startPos + (clickPos - startPos).normalized * distance;

        GameObject gm = Instantiate(checker, new Vector3(endPos.x, endPos.y, 0), Quaternion.identity);
        Rigidbody2D rbGm = gm.GetComponent<Rigidbody2D>();

        CheckerTrigger checkerGM = gm.GetComponent<CheckerTrigger>();

        Debug.Log($"1{rbGm.position}");
        int n = 5;

        while (n > 0)
        {
            Debug.Log($"2{rbGm.position}");
            if (checkerGM.checkWalls)
            {
                rbGm.position = Vector2.MoveTowards(rbGm.position, Vector2.zero, CheckerTrigger.step);
                Debug.Log($"3{rbGm.position}");
            }
            else if (checkerGM.checkFurnit)
            {
                rbGm.position = Vector2.MoveTowards(rbGm.position, endPos, CheckerTrigger.step);
                Debug.Log($"3{rbGm.position}");
            }
            n--;
        }
        //Destroy(gm);

        return rbGm.position;
    }

    private void EndMission()
    {
        if (item != null)
        {
            //item.enabled = true;
        }
        transform.DetachChildren();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            StopCoroutine(nameof(Throw));
            // подключиться к NPC и передать ему объект item
            Unit unit = collision.GetComponent<Unit>();
            if (unit != null)
            {
                unit.GetItem(item.index);
            }
            Destroy(item.gameObject);
            Destroy(gameObject);
            //EndMission();
        }

        //Debug.Log(collision.name);
    }
}