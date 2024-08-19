using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockController : MonoBehaviour
{
    public float probabilityWeight;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float landedLifespan;
    public GameObject blockType;
    public float width;
    public float height;

    // warning CS0108
#pragma warning disable
    private Collider2D collider;
#pragma warning restore

    private Rigidbody2D rb;

    private void Start()
    {
        collider = GetComponent<Collider2D>();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -fallSpeed);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Player")
        {
            PlayerBlockController pbc = otherCollider.gameObject.GetComponent<PlayerBlockController>();
            pbc.PickUpBlock(blockType, gameObject);
        }
        else if (otherCollider.tag == "Terrain")
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(DestroyAfterDelay(landedLifespan));
        }
        else if (otherCollider.tag == "Water")
        {
            StartCoroutine(DestroyAfterDelay(landedLifespan));
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
