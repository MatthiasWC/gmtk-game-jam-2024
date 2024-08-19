using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;

    private static float maxDeviance = float.PositiveInfinity;

    private Rigidbody2D rb;

    void Start()
    {
        Vector2 lowerMidPoint = (GameBounds.instance.lowerLeft + GameBounds.instance.lowerRight) / 2 - (new Vector2(0, GameBounds.instance.upperLeft.y - transform.position.y));
        Vector2 defaultDirection = (lowerMidPoint - new Vector2(transform.position.x, transform.position.y)).normalized;

        if (System.Single.IsPositiveInfinity(maxDeviance))
        {
            double opp = System.Math.Abs(GameBounds.instance.upperLeft.y - lowerMidPoint.y);
            double adj = System.Math.Abs(lowerMidPoint.x - GameBounds.instance.upperLeft.x);
            maxDeviance = (float)(System.Math.Abs(System.Math.Atan(opp / adj)) - 0.05);
        }

        float angleFromDefault = Random.Range(-maxDeviance, maxDeviance);
        Vector2 startDirection = new Vector2(
            (float)(defaultDirection.x * System.Math.Cos(angleFromDefault) - defaultDirection.y * System.Math.Sin(angleFromDefault)),
            (float)(defaultDirection.x * System.Math.Sin(angleFromDefault) + defaultDirection.y * System.Math.Cos(angleFromDefault))
        );
        Vector2 startVelocity = startDirection * Random.Range(minSpeed, maxSpeed);

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = startVelocity;
        rb.angularVelocity = Random.Range(0, 0.5f);
    }
}
