using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBounds : MonoBehaviour
{
    public static GameBounds instance;

    public Vector2 lowerLeft;
    public Vector2 lowerRight;
    public Vector2 upperRight;
    public Vector2 upperLeft;

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

        Vector2[] points = GetComponent<EdgeCollider2D>().points;

        lowerLeft = points[0];
        lowerRight = points[1];
        upperLeft = points[2];
        upperRight = points[3];
    }
}
