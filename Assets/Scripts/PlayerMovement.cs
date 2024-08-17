using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 15f;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newVelocity = rb.velocity;

        // move left/right
        newVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        if (HoldingJumpKey()) newVelocity.y = 1;

        // jump
        if (HoldingJumpKey() && IsGrounded())
        {
            newVelocity.y = jumpSpeed;
        }
        if (!HoldingJumpKey() && rb.velocity.y > 0)
        {
            newVelocity.y = 0;
        }

        rb.velocity = newVelocity;
    }

    bool HoldingJumpKey()
    {
        if (Input.GetKey(KeyCode.Space)) return true;
        if (Input.GetKey(KeyCode.W)) return true;
        return false;

    }

    bool IsGrounded()
    {
        Vector2 origin = col.bounds.center;

        float smallRadius = col.bounds.size.y - 0.05f;

        float bigRadius = col.bounds.size.y + 0.05f;

        if (Physics2D.CircleCast(origin, smallRadius, Vector2.down, bigRadius))
            return true;
        return false;

    }
}
