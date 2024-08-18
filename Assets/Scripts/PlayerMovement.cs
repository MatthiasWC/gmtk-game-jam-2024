using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float initialJumpSpeed = 5;
    public float maxJumpTime = 0.1f;
    public float earlyJumpReleaseVelocityModifier = 0.1f;
    public float coyoteTime = 0.1f;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private float lastWasGroundedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newVelocity = rb.velocity;
        SetGroundedTime();
        // move left/right
        newVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        // jump
        if (HoldingJumpKey() && IsGrounded() && rb.velocity.y <= 0.001f)
        {
            newVelocity.y = initialJumpSpeed;
            StartCoroutine(Jump());
        }

        rb.velocity = newVelocity;
    }

    IEnumerator Jump()
    {
        float currentJumpTime = 0f;

        while (currentJumpTime < maxJumpTime && HoldingJumpKey())
        {
            currentJumpTime += Time.deltaTime;
            yield return null;
        }
        if (!HoldingJumpKey() && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * earlyJumpReleaseVelocityModifier);
            yield break;
        }
    }


    bool PressedJumpKey()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return true;
        if (Input.GetKeyDown(KeyCode.W)) return true;
        return false;

    }

    bool HoldingJumpKey()
    {
        if (Input.GetKey(KeyCode.Space)) return true;
        if (Input.GetKey(KeyCode.W)) return true;
        return false;

    }

    bool ReleasedJumpKey()
    {
        if (Input.GetKeyUp(KeyCode.Space)) return true;
        if (Input.GetKeyUp(KeyCode.W)) return true;
        return false;
    }
    public bool IsGrounded()
    {
        if (Time.time - lastWasGroundedTime < coyoteTime)
        {
            return true;
        }
        return false;
    }

    void SetGroundedTime()
    {
        Vector2 origin = transform.position;
        origin.y = origin.y - col.bounds.extents.y - 0.1f;

        Collider2D otherCollider = Physics2D.OverlapBox(origin, new Vector2(col.bounds.size.x - 0.1f, 0.05f), 0);

        if (otherCollider == null) return;

        // Layer 8 is thrown blocks
        if (!otherCollider.CompareTag("Player") && otherCollider.gameObject.layer != 8)
        {
            lastWasGroundedTime = Time.time;
        }
    }
}
