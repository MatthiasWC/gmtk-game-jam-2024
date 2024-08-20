using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class waves_mechanic : MonoBehaviour
{
    public float risingSpeed = 1f;
    public float risingDelay;

    private float startTime;
    private Rigidbody2D rb;

    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        risingDelay = WavesCountdown.instance.prepTime;
    }

    void Update()
    {
        // begin rising
        if (Time.time - startTime > risingDelay)
        {
            rb.velocity = Vector2.up * risingSpeed;
        }
    }
}
