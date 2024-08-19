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
    [System.NonSerialized] public float rotation;
    private static float[] directions = new float[] { 0f, 90f, -90f, 180f };

    private void Start()
    {
        collider = GetComponent<Collider2D>();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -fallSpeed);

        rotation = directions[Random.Range(0, 4)];
        Transform child = transform.GetChild(0);
        SpriteRenderer childSR = child.gameObject.GetComponent<SpriteRenderer>();
        childSR.sprite = blockType.GetComponent<SpriteRenderer>().sprite;
        child.Rotate(0, 0, rotation);
        child.position += new Vector3(0, childSR.size.y / 2 * child.localScale.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Player")
        {
            PlayerBlockController pbc = otherCollider.gameObject.GetComponent<PlayerBlockController>();
            pbc.PickUpBlock(blockType, gameObject, rotation);
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
