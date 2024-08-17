using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocksController : MonoBehaviour
{
    /*[SerializeField] private int maxConcurrentBlocks;*/
    [SerializeField] private float blockFrequency;
    [SerializeField] private float blockIntervalVariation;
    [SerializeField] private List<GameObject> blockTypes;

    /*private List<GameObject> liveBlocks;*/

    void Start()
    {
        StartCoroutine(SpawnBlock());
    }

    IEnumerator SpawnBlock()
    {
        float waitTime = blockFrequency + Random.Range(-blockIntervalVariation, blockIntervalVariation);
        yield return new WaitForSeconds(waitTime);



        StartCoroutine(SpawnBlock());
    }
}
