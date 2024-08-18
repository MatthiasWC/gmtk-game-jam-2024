using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waves_animation : MonoBehaviour
{
    public float cycleTime = 2f;
    public float cycleDistance = 1f;
    public float cycleStartPosition = 0f;

    private float cycleStart = 0;
    private float transformStartX;

    private List<SpriteRenderer> sprites;
    // Start is called before the first frame update
    void Start()
    {
        transformStartX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPosition = transform.position;
        float elapsed = Time.time - cycleStart;
        float theta = Mathf.Lerp(0, 2 * Mathf.PI, elapsed / cycleTime);

        newPosition.x = transformStartX + Mathf.Sin(theta) * cycleDistance;
        transform.position = newPosition;

        // reset timer if necessary
        if (elapsed > cycleTime) cycleStart = Time.time;
    }
}
