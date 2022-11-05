using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private float verticalBounds = 20f;
    [SerializeField]
    private float horizontalBounds = 38f;
    private Rigidbody2D rb;

    public GameObject throwable;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) && ItemSystem.ItemInstance.isPicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Throw();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.position += Vector2.up * speed;
            if (rb.position.y > verticalBounds)
            {
                rb.position = new Vector2(rb.position.x, verticalBounds);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.position += Vector2.down * speed;
            if (rb.position.y < -verticalBounds)
            {
                rb.position = new Vector2(rb.position.x, -verticalBounds);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.position += Vector2.left * speed;
            if (rb.position.x < -horizontalBounds)
            {
                rb.position = new Vector2(-horizontalBounds, rb.position.y);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.position += Vector2.right * speed;
            if (rb.position.x > horizontalBounds)
            {
                rb.position = new Vector3(horizontalBounds, rb.position.y);
            }
        }

        /*if (Input.GetMouseButton(1) && ItemSystem.ItemInstance.isPicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Throw();
            }
        }*/
    }

    private void Throw()
    {
        Instantiate(throwable, transform.position, Quaternion.identity);
    }
}
