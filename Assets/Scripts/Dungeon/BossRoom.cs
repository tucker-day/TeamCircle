using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : ChildRoom
{
    public BossEnemySpawner bossEnemySpawner;
    public BossHPBarManager bossHPBarManager;
    [SerializeField]
    public FinalBoss boss;
    public GameObject PortalPrefab;
    protected override void SpawnEnemies()
    {
        GameManager.instance.SpawnEnemy(GameManager.instance.dungeonManager.settings.bossObject, transform.position);
        boss = FindObjectOfType<FinalBoss>();
        Instantiate(bossEnemySpawner.gameObject, transform.position, Quaternion.identity).GetComponent<BossEnemySpawner>().BossStats = boss;
        Instantiate(bossHPBarManager.gameObject).GetComponent<BossHPBarManager>().BossStats = boss;
    }

    public void Update()
    {
        if (boss.isAlive == false)
        {
            Instantiate(PortalPrefab, gameObject.transform);
        }
    }
}
