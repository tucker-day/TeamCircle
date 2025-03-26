using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour
{
    public FinalBoss BossStats;
    int RandomEnemySpawn;
    public float Timer = 0.0f;
    public float spawnTime = 2.0f;
    public float WaitTime = 2.0f;
    [SerializeField]
    private GameObject RangedEnemy;
    [SerializeField]
    private GameObject MeleeEnemy;



    public void SpawnEnemies()
    {
        RandomEnemySpawn = Random.Range(0, 3);
        if (RandomEnemySpawn == 0)
        {
            Debug.Log("melee enemy");
            Instantiate(MeleeEnemy);
        }
        if(RandomEnemySpawn == 1)
        {
            Debug.Log("ranged enemy");
            Instantiate(RangedEnemy);
        }
        if (RandomEnemySpawn == 2)
        {
            Debug.Log("tank enemy");
        }
    }

    void Update()
    {
        if (BossStats.isAlive == true)
        {
            Timer += Time.deltaTime;
            if (Timer > spawnTime)
            {
                SpawnEnemies();
                Timer = Timer - WaitTime;
            }
        }
    }
}
