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
            Vector2 throwVelocity = (mousePos - rb.position) * throwPower;

            Vector2[] throwTrajectory = PlotThrowTrajectory(throwVelocity);
            lr.positionCount = throwTrajectory.Length;
            Vector3[] positions = new Vector3[throwTrajectory.Length];
            for (int i = 0; i < throwTrajectory.Length; i++)
            {
                positions[i] = throwTrajectory[i];
            }
            lr.SetPositions(positions);

            if (Input.GetMouseButtonUp(0))
            {
                hasBeenThrown = true;
                transform.parent = null;
                rb.simulated = true;
                rb.velocity = throwVelocity;
            }
        }
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
}
