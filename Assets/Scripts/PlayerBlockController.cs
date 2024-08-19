using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockController : MonoBehaviour
{
    [SerializeField] float recoilForce = 0.1f;
    private GameObject currentBlock;
    private Rigidbody2D rb;
    private Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && currentBlock != null)
        {
            BlockController bc = currentBlock.GetComponent<BlockController>();
            Vector2 throwVelocity = bc.Throw();
            currentBlock = null;
            rb.velocity += throwVelocity.normalized * -recoilForce;

            if (col.IsTouchingLayers(LayerMask.GetMask("Falling Block")))
            {
                ContactFilter2D filter = new()
                {
                    useLayerMask = true,
                    layerMask = LayerMask.GetMask("Falling Block"),
                    useTriggers = true
                };
                Collider2D[] touchingColliders = new Collider2D[1];
                int numColliding = col.OverlapCollider(filter, touchingColliders);
                if (numColliding > 0)
                {
                    GameObject fallingBlock = touchingColliders[0].gameObject;
                    FallingBlockController fallingBlockController = fallingBlock.GetComponent<FallingBlockController>();
                    PickUpBlock(fallingBlockController.blockType, fallingBlock, fallingBlockController.rotation);
                }
            }
        }
    }

    public void PickUpBlock(GameObject block, GameObject fallingBlock, float rotation)
    {
        if (currentBlock == null)
        {
            float playerOffset = GetComponent<Collider2D>().bounds.extents.y;
            Vector3 offset = new Vector3(0, playerOffset, 0);
            currentBlock = Instantiate(block, transform.position + offset, Quaternion.Euler(0, 0, rotation));
            float blockOffset = currentBlock.GetComponent<Collider2D>().bounds.extents.y;
            currentBlock.transform.position += new Vector3(0, blockOffset, 0);
            currentBlock.AddComponent<FixedJoint2D>().connectedBody = rb;
            Destroy(fallingBlock);
        }
    }
}
