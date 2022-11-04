using UnityEngine;
public class ThrowSprite : MonoBehaviour
{
    public float moveSpeed;

    private Transform _transform;
    private GameObject _gameObject;

    private ItemPickUp item;

    Vector2 _startPos;
    Vector2 _endPos;
    bool _throwed;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ItemSystem.ItemInstance.isPicked = false;
            item = ItemSystem.ItemInstance.minItem;
            _transform = item.GetComponent<Transform>();
            item.GetComponent<BoxCollider2D>().isTrigger = false;
            _endPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            _throwed = true;
        }

        if (_throwed)
        {
            _transform.position = Vector2.MoveTowards(_transform.position, _endPos, moveSpeed * Time.deltaTime);

            if (Vector2.Equals(_endPos, (Vector2)_transform.position))
            {
                item.GetComponent<BoxCollider2D>().isTrigger = true;
                _throwed = false;
            }
        }
    }
}
