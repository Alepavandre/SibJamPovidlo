using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    #region Singleton

    public static ItemSystem ItemInstance;

    void Awake()
    {
        ItemInstance = this;
    }

    #endregion

    public List<ItemPickUp> items = new List<ItemPickUp>();
    public GameObject player;
    private float _distance = 100;
    [NonSerialized] public bool isEntered = false;

    private ItemPickUp minItem;

    private void Update()
    {
        if (!isEntered) return;

        if (items.Count == 0)
        {
            Debug.Log("List empty");
            isEntered = false;
            return;
        }

        if (items.Count == 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                minItem = items[0];
                minItem.TriggeredOneItem();
            }
            return;
        }

        if (items.Count > 1)
        {
            _distance = 100;

            foreach (var item in items)
            {
                float distance = Vector3.Distance(player.transform.position, item.gameObject.transform.position);
                Mathf.Abs(distance);

                if (_distance > distance)
                {
                    _distance = distance;
                    minItem = item;
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                minItem.TriggeredManyItems(minItem);
            }
        }
    }
}
