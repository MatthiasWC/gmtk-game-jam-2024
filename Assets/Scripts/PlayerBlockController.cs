using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockController : MonoBehaviour
{
    public void PickUpBlock(GameObject block)
    {
        Vector3 offset = new Vector3(0, block.GetComponent<Collider2D>().bounds.size.y / 2, 0);
        Object.Instantiate(block, offset, Quaternion.identity, transform);
    }
}
