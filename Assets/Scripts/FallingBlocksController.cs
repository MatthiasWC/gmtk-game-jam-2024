using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocksController : MonoBehaviour
{
    /*[SerializeField] private int maxConcurrentBlocks;*/
    [SerializeField] private float blockFrequency;
    [SerializeField] private float blockIntervalVariation;
    [SerializeField] private FallingBlockController[] blockTypes;

    private float probabilityRange;
    /*private List<GameObject> liveBlocks;*/

    void Start()
    {
        StartCoroutine(SpawnBlock());

        foreach (FallingBlockController blockType in blockTypes)
        {
            probabilityRange += blockType.probabilityWeight;
        }
    }

    IEnumerator SpawnBlock()
    {
        float waitTime = blockFrequency + Random.Range(-blockIntervalVariation, blockIntervalVariation);
        yield return new WaitForSeconds(waitTime);

        float targetBlockIndex = Random.Range(0, probabilityRange);
        float runningBlockIndex = 0;
        FallingBlockController chosenBlockType = blockTypes[blockTypes.Length - 1];
        foreach (FallingBlockController blockType in blockTypes)
        {
            runningBlockIndex += blockType.probabilityWeight;
            if (targetBlockIndex <= runningBlockIndex)
            {
                chosenBlockType = blockType;
                break;
            }
        }

        Vector2[] spawnRange = new[] { GameBounds.instance.upperLeft, GameBounds.instance.upperRight };

        float halfBlockWidth = chosenBlockType.width / 2;
        float halfBlockHeight = chosenBlockType.height / 2;
        float spawnX = Random.Range(spawnRange[0].x + halfBlockWidth, spawnRange[1].x - halfBlockWidth);
        Vector3 spawnPos = new Vector3(spawnX, spawnRange[0].y + halfBlockHeight + 0.1f, 0);
        Object.Instantiate(chosenBlockType, spawnPos, Quaternion.identity);

        StartCoroutine(SpawnBlock());
    }
}
