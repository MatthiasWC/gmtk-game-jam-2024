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
    public float maxFallSpeed = 10f;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private float lastWasGroundedTime = 0f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newVelocity = rb.velocity;
        SetGroundedTime();
        // move left/right
        newVelocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        if (newVelocity.x < -0.01f)
            transform.localScale = new Vector3(-System.Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (newVelocity.x > 0.01f)
            transform.localScale = new Vector3(System.Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        animator.SetFloat("xSpeed", System.Math.Abs(newVelocity.x));

        // jump
        if (HoldingJumpKey() && IsGrounded() && rb.velocity.y <= 0.001f)
        {
            newVelocity.y = initialJumpSpeed;
            StartCoroutine(Jump());
        }

        rb.velocity = newVelocity;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < -maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);
        }
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
        bool isGrounded = Time.time - lastWasGroundedTime < coyoteTime;
        return isGrounded;
    }

    void SetGroundedTime()
    {
        Vector2 origin = col.bounds.center;
        origin.y -= col.bounds.extents.y;

        /*LayerMask layerMask = LayerMask.NameToLayer("Terrain");
        Debug.Log("layerMask: " + LayerMask.NameToLayer("Terrain"));
        Collider2D otherCollider = Physics2D.OverlapBox(origin, new Vector2(col.bounds.size.x - 0.03f, 4f), 0, LayerMask.NameToLayer("Terrain"));
        otherCollider = Physics2D.OverlapBox(origin, new Vector2(col.bounds.size.x - 0.03f, 4f), 0, LayerMask.NameToLayer("Terrain"));
        Debug.Log("other collider: " + otherCollider);*/

        // Layermasks aren't working with overlap boxes so we just have to get all colliders and check each one manually
        Collider2D[] otherColliders = Physics2D.OverlapBoxAll(origin, new Vector2(col.bounds.size.x - 0.03f, 0.1f), 0);
        for (int i = 0; i < otherColliders.Length; i++)
        {
            if (otherColliders[i].gameObject.layer == LayerMask.NameToLayer("Terrain"))
            {
                lastWasGroundedTime = Time.time;
                animator.SetBool("Grounded", true);
                return;
            }
        }
        animator.SetBool("Grounded", false);
    }

    /*private void OnDrawGizmos()
    {
        Vector2 origin = col.bounds.center;
        origin.y -= col.bounds.extents.y;

        Gizmos.color = Color.green;
        Gizmos.DrawCube(origin, new Vector3(col.bounds.size.x - 0.03f, 0.1f, 0));
    }*/
}
