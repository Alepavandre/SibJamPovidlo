using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private float verticalBounds = 20f;
    [SerializeField]
    private float horizontalBounds = 38f;
    private Rigidbody2D rb;

    [SerializeField] private float rotationSpeed = 100f;
    
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
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(deltaX, deltaY);
        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);
        moveDirection.Normalize();

        transform.Translate(moveDirection * speed * inputMagnitude * Time.fixedDeltaTime, Space.World);
        
        if (moveDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        /*if (Input.GetKey(KeyCode.W))
        {
            rb.position += Vector2.up * speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (rb.position.y > verticalBounds)
            {
                rb.position = new Vector2(rb.position.x, verticalBounds);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {

            rb.position += Vector2.down * speed;
            transform.rotation = Quaternion.Euler(0, 0, 180);
            if (rb.position.y < -verticalBounds)
            {
                rb.position = new Vector2(rb.position.x, -verticalBounds);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.position += Vector2.left * speed;
            transform.rotation = Quaternion.Euler(0, 0, 90);
            if (rb.position.x < -horizontalBounds)
            {
                rb.position = new Vector2(-horizontalBounds, rb.position.y);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.position += Vector2.right * speed;
            transform.rotation = Quaternion.Euler(0, 0, -90);
            if (rb.position.x > horizontalBounds)
            {
                rb.position = new Vector3(horizontalBounds, rb.position.y);
            }
        }*/

    }

    private void Throw()
    {
        Instantiate(throwable, transform.position, Quaternion.identity);
    }
}
