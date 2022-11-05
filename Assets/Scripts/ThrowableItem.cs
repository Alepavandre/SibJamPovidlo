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
        Vector2 startPos = rb.position;
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 endPos = startPos + (clickPos - startPos).normalized * distance;

        for (float i = 0f; i < distance; i += speedItem)
        {
            yield return new WaitForSeconds(.01f);
            rb.position = Vector2.MoveTowards(rb.position, endPos, speedItem);
        }
        EndMission();
    }

    private void EndMission()
    {
        if (item != null)
        {
            item.enabled = true;
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
        if (collision.CompareTag("Walls"))
        {
            StopCoroutine(nameof(Throw));
            EndMission();
        }
    }
}
