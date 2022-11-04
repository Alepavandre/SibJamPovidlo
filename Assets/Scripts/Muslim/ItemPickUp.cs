using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemSystem.ItemInstance.isEntered = true;
            ItemSystem.ItemInstance.items.Add(this);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemSystem.ItemInstance.isEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemSystem.ItemInstance.isEntered = false;
            if (ItemSystem.ItemInstance.items.Count > 0)
            {
                ItemSystem.ItemInstance.items.Remove(this);
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
        ItemSystem.ItemInstance.isEntered = false;
        Destroy(gameObject);
    }
}
