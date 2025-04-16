using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : ChildRoom
{
    public BossEnemySpawner bossEnemySpawner;
    public BossHPBarManager bossHPBarManager;

    protected override void SpawnEnemies()
    {
        GameManager.instance.SpawnEnemy(GameManager.instance.dungeonManager.settings.bossObject, transform.position);
        FinalBoss boss = FindObjectOfType<FinalBoss>();
        Instantiate(bossEnemySpawner.gameObject, transform.position, Quaternion.identity).GetComponent<BossEnemySpawner>().BossStats = boss;
        Instantiate(bossHPBarManager.gameObject).GetComponent<BossHPBarManager>().BossStats = boss;
    }
}
