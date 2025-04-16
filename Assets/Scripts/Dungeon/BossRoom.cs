using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : ChildRoom
{
    public BossEnemySpawner bossEnemySpawner;

    protected override void SpawnEnemies()
    {
        GameManager.instance.SpawnEnemy(GameManager.instance.dungeonManager.settings.bossObject, transform.position);
        Instantiate(bossEnemySpawner.gameObject, transform.position, Quaternion.identity).GetComponent<BossEnemySpawner>().BossStats = FindObjectOfType<FinalBoss>();
    }
}
