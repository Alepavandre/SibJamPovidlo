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
    public float step = 2f;

    [NonSerialized] public bool isEntered = false;
    [NonSerialized] public bool isPicked = false;

    [NonSerialized] public ItemPickUp minItem;

    private void Update()
    {
        if (!isEntered) return;

        if (isPicked)
        {
            //minItem.transform.position = player.transform.position;
            //minItem.transform.SetParent(player.transform);

            if (Input.GetMouseButtonDown(0)) 
            {
                //isPicked = false;
            }
            
            return;
        }

        if (items.Count == 0)
        {
            //Debug.Log("List empty");
            isEntered = false;
            return;
        }

        if (items.Count == 1)
        {
            minItem = items[0];
            if (minItem == null)
            {
                return;
            }
            minItem.ChangeSpriteColor();

            if (Input.GetKeyDown(KeyCode.E))
            {
                minItem.TriggeredOneItem();
                SetPicked();
            }
            return;
        }

        if (items.Count > 1)
        {
            _distance = 100;
            foreach (var item in items)
            {
                if (item == null)
                {
                    items.Remove(item);
                    Debug.Log("Item is missing!");
                    return;
                }

                float distance = Vector3.Distance(player.transform.position, item.gameObject.transform.position);
                distance = Mathf.Abs(distance);

                if (_distance > distance)
                {
                    if (minItem == null)
                    {
                        minItem = item;
                    }

                    minItem.ChangeSpriteColor(true); //вернуть обратно цвет у прошлого объекта
                    minItem = item;
                    minItem.ChangeSpriteColor(); // поменять на активынй цвет у объекта который теперь ближе
                    _distance = distance;
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                minItem.TriggeredManyItems(minItem);
                SetPicked();
            }
        }
    }

    private void SetPicked()
    {
        if (minItem.transform.childCount != 0)
        {
            Destroy(minItem.transform.GetChild(0).gameObject);
        }
        if (minItem.isInfinity)
        {
            minItem = Instantiate(minItem, player.transform.position, Quaternion.identity, player.transform);
            minItem.isInfinity = false;
        }
        else
        {
            minItem.transform.position = player.transform.position;
            minItem.transform.SetParent(player.transform);
        }
        if (minItem.transform.childCount != 0)
        {
            Destroy(minItem.transform.GetChild(0).gameObject);
        }
        isPicked = true;
    }
}
