using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorManager : MonoBehaviour
{
    [SerializeField] private GameObject meteor;
    [SerializeField] private float meteorFrequency;
    [SerializeField] private float meteorIntervalVariation;
    [SerializeField] private float meteorElevationMultiplier = 0.1f;
    [SerializeField] private float lowestMeteorHeight = 6;

    private float ySpawnRange;
    private float xSpawnRange;
    private float meteorRadius;
    private GameBounds gameBounds;
    private Transform player;

    void Start()
    {
        meteorRadius = meteor.GetComponent<CircleCollider2D>().radius + 1;

        gameBounds = GameBounds.instance;

        player = PlayerSingleton.instance.gameObject.transform;

        ySpawnRange = gameBounds.upperLeft.y - gameBounds.lowerLeft.y - lowestMeteorHeight;
        xSpawnRange = gameBounds.upperRight.x - gameBounds.upperLeft.x;

        StartCoroutine(SpawnMeteor());
    }

    IEnumerator SpawnMeteor()
    {
        float waitTime = meteorFrequency + Random.Range(-meteorIntervalVariation, meteorIntervalVariation) - player.position.y * meteorElevationMultiplier;
        yield return new WaitForSeconds(waitTime > 0 ? waitTime : 0);

        float spawnDist = Random.Range(0, ySpawnRange * 2 + xSpawnRange);

        Vector2 spawnPos = Vector2.zero;
        if (spawnDist <= ySpawnRange)
        {
            spawnPos = new Vector2(gameBounds.upperLeft.x - meteorRadius, gameBounds.upperLeft.y - spawnDist);
        }
        else if (spawnDist <= ySpawnRange + xSpawnRange)
        {
            spawnPos = new Vector2(gameBounds.upperLeft.x + spawnDist - ySpawnRange, gameBounds.upperLeft.y + meteorRadius);
        }
        else
        {
            spawnPos = new Vector2(gameBounds.upperRight.x + meteorRadius, gameBounds.upperRight.y - (spawnDist - ySpawnRange - xSpawnRange));
        }

        Object.Instantiate(meteor, spawnPos, Quaternion.identity);

        StartCoroutine(SpawnMeteor());
    }
}
