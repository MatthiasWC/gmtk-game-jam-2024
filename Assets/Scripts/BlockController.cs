using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Vector2 size;
    [SerializeField] private float throwPower = 5f;
    [SerializeField] private int trajectoryLineSteps = 500;

    private bool hasBeenThrown;
    private Vector2 mousePos;
    private Vector2 throwVelocity;
    private Rigidbody2D rb;
    private LineRenderer lr;
    private Destructible db;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        db = GetComponent<Destructible>();
        /*db.AddCallback(BeforeDestruct);*/
    }

    private void Update()
    {
        if (!hasBeenThrown)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            throwVelocity = (mousePos - new Vector2(transform.position.x, transform.position.y)) * throwPower;

            Vector2[] throwTrajectory = PlotThrowTrajectory(throwVelocity);
            lr.positionCount = throwTrajectory.Length;
            Vector3[] positions = new Vector3[throwTrajectory.Length];
            for (int i = 0; i < throwTrajectory.Length; i++)
            {
                positions[i] = throwTrajectory[i];
            }
            lr.SetPositions(positions);
        }
    }

    public Vector2 Throw()
    {
        hasBeenThrown = true;
        Destroy(gameObject.GetComponent<FixedJoint2D>());
        lr.enabled = false;
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Terrain")))
        {
            DisablePhysics();
            gameObject.layer = LayerMask.NameToLayer("Terrain");
            return Vector2.zero;
        }
        EnablePhysics();
        rb.velocity = throwVelocity;
        return throwVelocity;
    }

    private void EnablePhysics()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void DisablePhysics()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }

    private Vector2[] PlotThrowTrajectory(Vector2 velocity)
    {
        Vector2[] results = new Vector2[trajectoryLineSteps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rb.gravityScale * timestep * timestep;

        Vector2 moveStep = velocity * timestep;

        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        for (int i = 0; i < trajectoryLineSteps; i++)
        {
            moveStep += gravityAccel;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasBeenThrown && collision.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            DisablePhysics();
            /*RelativeJoint2D joint = gameObject.AddComponent<RelativeJoint2D>();//.connectedBody = collision.collider.gameObject.GetComponent<Rigidbody2D>();
            joint.connectedBody = collision.collider.gameObject.GetComponent<Rigidbody2D>();*/
            /*joint.dampingRatio = 1;*/
            /*joint.maxForce = 1000;*/
            gameObject.layer = LayerMask.NameToLayer("Terrain");
        }
    }

    /*private void BeforeDestruct()
    {
        Debug.Log("Before destruct");
        Collider2D col = GetComponent<Collider2D>();
        ContactFilter2D filter = new()
        {
            useLayerMask = true,
            layerMask = LayerMask.GetMask("Terrain")
        };
        Collider2D[] touchingColliders = new Collider2D[100];
        int numColliding = col.OverlapCollider(filter, touchingColliders);
        for (int i = 0; i < numColliding; i++)
        {
            BlockController bc = touchingColliders[i].gameObject.GetComponent<BlockController>();
            if (bc != null)
            {
                bc.AdjacentBlockDestroyed();
            }
        }
    }

    private void AdjacentBlockDestroyed()
    {
        Debug.Log("adjacent block destroyed");
        Collider2D col = GetComponent<Collider2D>();
        ContactFilter2D filter = new()
        {
            useLayerMask = true,
            layerMask = LayerMask.GetMask("Terrain")
        };
        Collider2D[] touchingColliders = new Collider2D[1];
        int numColliding = col.OverlapCollider(filter, touchingColliders);
        if (numColliding == 0)
        {
            EnablePhysics();
        }
    }*/
}
