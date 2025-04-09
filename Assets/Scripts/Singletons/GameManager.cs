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

    private GameObject player;

    public DungeonManager dungeonManager;


    public bool minibossPresent = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        DebugSpawnPickups();
    }

    // Check the static enemy list to see if enemies are present.
    public bool CheckForEnemies()
    {
#if UNITY_EDITOR
        Debug.Log("Current Enemy Count: " + Enemy.s_enemyList.Count);
#endif
        if (Enemy.s_enemyList.Count >= 1)
        {
#if UNITY_EDITOR
            // Debug.Log("THERE BE ENEMIES HERE!");
#endif
            return true;
        }
        else
        {
#if UNITY_EDITOR
            // Debug.Log("No enemies detected. All clear!");
#endif
            return false;
        }
    }

    public void SpawnEnemy(GameObject enemyObj, Vector2 pos)
    {
        if (enemyObj.TryGetComponent(out Enemy enemy))
        {
            GameObject instance = Instantiate(enemyObj, pos, Quaternion.identity);
            instance.GetComponent<Enemy>().hp = Mathf.FloorToInt((float)enemy.hp * dungeonManager.settings.enemyHealthMult);
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
}
