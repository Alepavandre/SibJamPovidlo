using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    #region Singleton

    public static ItemSystem instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public List<ItemPickUp> items = new List<ItemPickUp>();
    public GameObject player;
    public float _distance = -1;
    public bool isEntered = false;

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
                //Debug.Log($"OneItem: {items.First()} + {_distance}");
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
                    Debug.Log($"MinItem: {minItem} + {_distance}");
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                minItem.TriggeredManyItems(minItem);
            }
        }
    }
}
