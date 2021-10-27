using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    static public EnemiesManager instance;

    public GameObject npcSpawn;
    public GameObject[] enemies;
    public float initialTimeBetweenSpawn;
    public float minimalTimeBetweenSpawn;
    public float timeReduce;
    public float timeBeforeFirstSpawn;

    private PolygonCollider2D spawnArea;
    private bool enemySpawning = false;
    private float timeBetweenSpawn;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spawnArea = GetComponent<PolygonCollider2D>();
        timeBetweenSpawn = initialTimeBetweenSpawn;
    }

    public void startSpawning()
    {
        enemySpawning = true;
        StartCoroutine(manageSpawn());
    }

    public void stopSpawning()
    {
        enemySpawning = false;
    }

    private IEnumerator manageSpawn()
    {
        yield return new WaitForSeconds(timeBeforeFirstSpawn);

        while (enemySpawning)
        {
            int enemiesNumber = UnityEngine.Random.Range(1, 4);
            for (int i = 0; i < enemiesNumber; i++)
            {
                spawnEnemyRandomly(enemies[UnityEngine.Random.Range(0, enemies.Length)]);
            }

            timeBetweenSpawn -= timeReduce;
            if (timeBetweenSpawn < minimalTimeBetweenSpawn)
                timeBetweenSpawn = minimalTimeBetweenSpawn;

            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }

    private void spawnEnemyRandomly(GameObject enemy)
    {
        Vector3 rndPoint3D = RandomPointInBounds(spawnArea.bounds, 1f);
        Vector2 rndPoint2D = new Vector2(rndPoint3D.x, rndPoint3D.y);
        Vector2 rndPointInside = spawnArea.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));
        if (rndPointInside.x == rndPoint2D.x && rndPointInside.y == rndPoint2D.y)
        {
            var spawn = Instantiate(npcSpawn, rndPoint3D, Quaternion.identity);
            spawn.GetComponent<NpcSpawn>().NpcToSpawn = enemy;
        }
        else
        {
            spawnEnemyRandomly(enemy);
        }
    }

    private Vector3 RandomPointInBounds(Bounds bounds, float scale)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x * scale, bounds.max.x * scale),
            UnityEngine.Random.Range(bounds.min.y * scale, bounds.max.y * scale),
            1f
        );
    }
}
