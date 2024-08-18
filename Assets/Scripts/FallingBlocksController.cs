using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocksController : MonoBehaviour
{
    /*[SerializeField] private int maxConcurrentBlocks;*/
    [SerializeField] private float blockFrequency;
    [SerializeField] private float blockIntervalVariation;
    [SerializeField] private FallingBlockController[] blockTypes;

    private Vector2[] spawnRange;
    private float probabilityRange;
    /*private List<GameObject> liveBlocks;*/

    void Start()
    {
        StartCoroutine(SpawnBlock());

        GameBounds gameBounds = GameBounds.instance;
        spawnRange = new[] { gameBounds.upperLeft, gameBounds.upperRight + new Vector2(0, 5) };

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

        float halfBlockWidth = chosenBlockType.width / 2;
        float halfBlockHeight = chosenBlockType.height / 2;
        float spawnX = Random.Range(spawnRange[0].x + halfBlockWidth, spawnRange[1].x - halfBlockWidth);
        Vector3 spawnPos = new Vector3(spawnX, spawnRange[0].y + halfBlockHeight, 0);
        Object.Instantiate(chosenBlockType, spawnPos, Quaternion.identity);

        StartCoroutine(SpawnBlock());
    }
}
