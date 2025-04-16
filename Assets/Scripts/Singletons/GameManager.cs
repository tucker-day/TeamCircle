using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject healthPickup;
    [SerializeField]
    private GameObject weaponPickup;
    [SerializeField]
    private GameObject chest;

    private GameObject player;

    public DungeonManager dungeonManager;
    public int rareEnemyChance = 300;
    int rareEnemyChanceIncrease = 3;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize an instance of the game manager.
        if (dungeonManager != null)
           {
             dungeonManager.GenerateDungeon();
           }
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    

        player = GameObject.FindGameObjectWithTag("Player");
        CheckForEnemies();
        CheckForRareEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        DebugSpawnPickups();
    }

    // Check the static enemy list to see if enemies are present.
    public bool CheckForEnemies()
    {
        if (Enemy.s_enemyList.Count >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckForRareEnemies()
    {
        if (Enemy.s_rareEnemyList.Count >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SpawnEnemy(GameObject enemyObj, Vector2 pos)
    {
        if (enemyObj.TryGetComponent(out Enemy enemy))
        {
            GameObject instance = Instantiate(enemyObj, pos, Quaternion.identity);

            int rareEnemySpawn = Random.Range(0, rareEnemyChance + rareEnemyChanceIncrease);
            if (rareEnemySpawn >= rareEnemyChance)
            {
                instance.GetComponent<Enemy>().isRareEnemy = true;
            }
        }
        else
        {
            Debug.LogError("SpawnEnemy was told to spawn something that wasn't an enemy!");
        }
    }

    public void GameOver()
    {

    }

    public void PauseGame()
    {

    }

    public void SpawnHealthPickup(Vector3 enemyPos)
    {
        Object.Instantiate(healthPickup, enemyPos, Quaternion.identity);
    }

    public void SpawnWeaponPickup(Vector3 enemyPos)
    {
        Object.Instantiate(weaponPickup, enemyPos, Quaternion.identity);
    }

    public void SpawnChest(Vector3 enemyPos)
    {
        Object.Instantiate(chest, enemyPos, Quaternion.identity);
    }

    // Debug function for spawning pickups.
    void DebugSpawnPickups()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 spawnRange = new Vector3(player.transform.position.x + Random.Range(-3, 3),
                player.transform.position.y + Random.Range(-3, 3), player.transform.position.z);
            Object.Instantiate(healthPickup, spawnRange, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Vector3 spawnRange = new Vector3(player.transform.position.x + Random.Range(-3, 3),
                player.transform.position.y + Random.Range(-3, 3), player.transform.position.z);
            Object.Instantiate(weaponPickup, spawnRange, Quaternion.identity);
        }
    }

    public void FinishFloor()
    {
        // Increase enemy health multiplier by 1.33.
        // Increase rare enemy spawn chance by 1 to 3.
    }
}
