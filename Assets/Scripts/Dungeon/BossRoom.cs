using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : ChildRoom
{
    public BossEnemySpawner bossEnemySpawner;
    public BossHPBarManager bossHPBarManager;
    private FinalBoss boss;
    [SerializeField]
    public GameObject PortalPrefab;
    private bool bossSpawned = false;
    protected override void SpawnEnemies()
    {
        GameManager.instance.SpawnEnemy(GameManager.instance.dungeonManager.settings.bossObject, transform.position);
        boss = FindObjectOfType<FinalBoss>();
        bossSpawned = true;
        Instantiate(bossEnemySpawner.gameObject, transform.position, Quaternion.identity).GetComponent<BossEnemySpawner>().BossStats = boss;
        Instantiate(bossHPBarManager.gameObject).GetComponent<BossHPBarManager>().BossStats = boss;
    }

    public void Update()
    {
        if (bossSpawned)
        {
            if (boss == null)
            {
                bossSpawned=false;
                Instantiate(PortalPrefab, gameObject.transform);
            }
        }
    }
}
