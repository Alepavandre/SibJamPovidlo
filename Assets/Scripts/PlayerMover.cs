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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed;
            if (transform.position.y > verticalBounds)
            {
                transform.position = new Vector3(transform.position.x, verticalBounds);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * speed;
            if (transform.position.y < -verticalBounds)
            {
                transform.position = new Vector3(transform.position.x, -verticalBounds);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed;
            if (transform.position.x < -horizontalBounds)
            {
                transform.position = new Vector3(-horizontalBounds, transform.position.y);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed;
            if (transform.position.x > horizontalBounds)
            {
                transform.position = new Vector3(horizontalBounds, transform.position.y);
            }
        }
    }
}
