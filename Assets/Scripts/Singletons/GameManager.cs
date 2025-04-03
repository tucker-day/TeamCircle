using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool minibossPresent = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize an instance of the game manager.
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        CheckForEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check the static enemy list to see if enemies are present.
    public bool CheckForEnemies()
    {
        if (Enemy.s_enemyList.Count >= 1)
        {
#if UNITY_EDITOR
            Debug.Log("THERE BE ENEMIES HERE!");
#endif
            return true;
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("No enemies detected. All clear!");
#endif
            return false;
        }
    }

    public void SpawnEnemies()
    {

    }

    public void GameOver()
    {

    }

    public void PauseGame()
    {

    }
}
