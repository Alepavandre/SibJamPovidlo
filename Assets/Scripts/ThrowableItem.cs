using System.Collections;
using UnityEngine;

public class ThrowableItem : MonoBehaviour
{
    [SerializeField] private float distance = 13f;
    [SerializeField] private float speedItem = 0.5f;
    [SerializeField] private GameObject checker;
    private ItemPickUp item;
    private Rigidbody2D rb;

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

        Debug.Log($"endpos {endPos}");
        Debug.Log($"1{rbGm.position}");
        
        float deviation = 30f;
        float dis = distance + deviation;

        while (dis > 0)
        {  
            RaycastHit2D hit = Physics2D.Raycast(rbGm.position, Vector2.zero, 0.1f);

            if (hit.collider == null)
            {
                Debug.Log($"null {rbGm.position}");
                Destroy(gm);
                return rbGm.position;
            }
            else if (hit.collider.CompareTag("Walls") || hit.collider.CompareTag("Furnit"))
            {
                rbGm.position = Vector2.MoveTowards(rbGm.position, Vector2.zero, ItemSystem.ItemInstance.step);
                Debug.Log($"3{rbGm.position}");
            }
            /*else if (hit.collider.CompareTag("Furnit"))
            {
                rbGm.position = Vector2.MoveTowards(rbGm.position, endPos, CheckerTrigger.step);
                Debug.Log($"4{rbGm.position}");
            }*/
            dis -= Time.deltaTime;
        }
        Destroy(gm);
        if (dis + deviation > 0) distance += deviation; 
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