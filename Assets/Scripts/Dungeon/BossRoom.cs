using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : ChildRoom
{
    public BossEnemySpawner bossEnemySpawner;
    public BossHPBarManager bossHPBarManager;

    protected override void SpawnEnemies(ChildRoom room, Vector2 roomPos)
    {
        GameManager.instance.SpawnEnemy(GameManager.instance.dungeonManager.settings.bossObject, roomPos);
        FinalBoss boss = FindObjectOfType<FinalBoss>();
        Instantiate(bossEnemySpawner.gameObject, roomPos, Quaternion.identity).GetComponent<BossEnemySpawner>().BossStats = boss;
        Instantiate(bossHPBarManager.gameObject).GetComponent<BossHPBarManager>().BossStats = boss;
    }
}
