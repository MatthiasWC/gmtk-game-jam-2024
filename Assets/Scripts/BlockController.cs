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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
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
        /*transform.parent = null;*/
        Destroy(gameObject.GetComponent<FixedJoint2D>());
        lr.enabled = false;
        /*rb.simulated = true;*/
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = throwVelocity;
        return throwVelocity;
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
        if (hasBeenThrown && collision.collider.gameObject.layer == 7)
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.bodyType = RigidbodyType2D.Static;
            // layer 7 is terrain
            gameObject.layer = 7;
        }
    }
}
