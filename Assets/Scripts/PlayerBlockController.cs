using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockController : MonoBehaviour
{
    private GameObject currentBlock;
    private LineRenderer trajectoryLine;
    /*private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }*/

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && currentBlock != null)
        {
            BlockController bc = currentBlock.GetComponent<BlockController>();
            bc.Throw();
            currentBlock = null;
        }
    }

    public void PickUpBlock(GameObject block, GameObject fallingBlock)
    {
        if (currentBlock == null)
        {
            float playerOffset = GetComponent<Collider2D>().bounds.extents.y;
            float blockOffset = block.GetComponent<BlockController>().size.y / 2;
            Vector3 offset = new Vector3(0, playerOffset + blockOffset, 0);
            currentBlock = Instantiate(block, transform.position + offset, Quaternion.identity, transform);
            /*currentBlock.AddComponent<FixedJoint2D>();
            currentBlock.GetComponent<FixedJoint2D>().connectedBody = rb;*/
            Destroy(fallingBlock);
        }
    }
}
