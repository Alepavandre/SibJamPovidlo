using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private Color spriteOriginalColor;
    private Color spriteChangeColor = Color.black;


    private void Start()
    {
        spriteOriginalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItemSystem.ItemInstance.items.Add(this);
            ItemSystem.ItemInstance.isEntered = true;
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
            ChangeSpriteColor(true);
            if (ItemSystem.ItemInstance.items.Count > 0)
            {
                ItemSystem.ItemInstance.items.Remove(this);
            }
        }
    }

    public void TriggeredOneItem()
    {
        Debug.Log($"PickedUp onece: {this}");
        ChangeSpriteColor(true);
        ItemSystem.ItemInstance.isEntered = false;
    }

    public void TriggeredManyItems(ItemPickUp item)
    {
        Debug.Log($"PickedUp one of two: {item}");
        ChangeSpriteColor(true);
        ItemSystem.ItemInstance.isEntered = false;
    }

    public void ChangeSpriteColor(bool backLoadSprite = false)
    {
        if (backLoadSprite)
        {
            gameObject.GetComponent<SpriteRenderer>().color = spriteOriginalColor;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = spriteChangeColor;
        }
    }
}
