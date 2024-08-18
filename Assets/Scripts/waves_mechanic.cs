using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class waves_mechanic : MonoBehaviour
{
    public float risingSpeed = 1f;
    public float risingDelay = 5f;

    private float startTime;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // begin rising
        if (Time.time - startTime > risingDelay)
        {
            rb.velocity = Vector2.up * risingSpeed;
        }

    }




}
