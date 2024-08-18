using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockController : MonoBehaviour
{
    public float probabilityWeight;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float landedLifespan;
    [SerializeField] private GameObject blockType;

    private Collider2D collider;
    [System.NonSerialized] public float width;
    [System.NonSerialized] public float height;

    private Rigidbody2D rb;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        width = collider.bounds.size.x;
        width = collider.bounds.size.y;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -fallSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider?.tag == "Player")
        {
            PlayerBlockController pbc = collision.gameObject.GetComponent<PlayerBlockController>();
            pbc.PickUpBlock(blockType);
            Object.Destroy(gameObject);
        }
        else if (collision.collider?.tag == "Terrain")
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(DestroyAfterDelay(landedLifespan));
        }
        else if (collision.collider?.tag == "Water")
        {
            StartCoroutine(DestroyAfterDelay(landedLifespan));
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (gameObject != null)
        {
            Object.Destroy(gameObject);
        }
    }
}
