using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;

    public Vector2 lsatDir;
    //References
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lsatDir = new Vector2(1f, 0f);

    }

    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lsatDir = new Vector2(lastHorizontalVector, 0f);
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lsatDir =  new Vector2(0f, lastVerticalVector);
        }

        if (moveDir.y != 0&&moveDir.x != 0)
        {
            lsatDir = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}