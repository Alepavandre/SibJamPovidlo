using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemSystem.instance.isEntered = true;
            ItemSystem.instance.items.Add(this);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemSystem.instance.isEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemSystem.instance.isEntered = false;
            if (ItemSystem.instance.items.Count > 0)
            {
                ItemSystem.instance.items.Remove(this);
            }
        }
    }

    public void TriggeredOneItem()
    {
        Debug.Log($"PickedUp onece: {this}");
        Destroy(gameObject);
    }

    public void TriggeredManyItems(ItemPickUp item)
    {
        Debug.Log($"PickedUp one of two: {item}");
        ItemSystem.instance.isEntered = false;
        Destroy(gameObject);
    }
}
