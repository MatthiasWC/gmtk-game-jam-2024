using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockController : MonoBehaviour
{
    [SerializeField] float recoilForce = 0.1f;
    private GameObject currentBlock;
    private LineRenderer trajectoryLine;
    private Rigidbody2D rb;
    private PlayerMovement movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && currentBlock != null)
        {
            BlockController bc = currentBlock.GetComponent<BlockController>();
            Vector2 throwVelocity = bc.Throw();
            currentBlock = null;
            rb.velocity += throwVelocity.normalized * -recoilForce;
        }
    }

    public void PickUpBlock(GameObject block, GameObject fallingBlock)
    {
        if (currentBlock == null)
        {
            float playerOffset = GetComponent<Collider2D>().bounds.extents.y;
            float blockOffset = block.GetComponent<BlockController>().size.y / 2;
            Vector3 offset = new Vector3(0, playerOffset + blockOffset, 0);
            currentBlock = Instantiate(block, transform.position + offset, Quaternion.identity);
            currentBlock.AddComponent<FixedJoint2D>();
            currentBlock.GetComponent<FixedJoint2D>().connectedBody = rb;
            Destroy(fallingBlock);
        }
    }
}
