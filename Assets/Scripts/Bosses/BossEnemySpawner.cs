using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour
{
    public FinalBoss BossStats;
    public float Timer = 0.0f;
    public float spawnTime = 2.0f;

    public void SpawnEnemies()
    {
        GameManager.instance.dungeonManager.settings.bossSpawnPool.GetRandomEnemy(out GameObject enemy, out int cost);
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        GameManager.instance.SpawnEnemy(enemy, enemyPos);
        Timer -= spawnTime;
    }

    void Update()
    {
        if (BossStats.isAlive == true)
        {
            Timer += Time.deltaTime;
            if (Timer > spawnTime)
            {
                SpawnEnemies();
                Timer = Timer - spawnTime;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
