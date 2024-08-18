using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBounds : MonoBehaviour
{
    public static GameBounds instance;

    public Vector2 lowerLeft
    {
        get => points[0] + new Vector2(transform.position.x, transform.position.y);
    }
    public Vector2 lowerRight
    {
        get => points[1] + new Vector2(transform.position.x, transform.position.y);
    }
    public Vector2 upperRight
    {
        get => points[2] + new Vector2(transform.position.x, transform.position.y);
    }
    public Vector2 upperLeft
    {
        get => points[3] + new Vector2(transform.position.x, transform.position.y);
    }

    private Vector2[] points;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        points = GetComponent<EdgeCollider2D>().points;
    }
}
