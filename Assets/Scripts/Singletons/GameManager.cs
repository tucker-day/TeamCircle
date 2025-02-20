using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool enemiesPresent = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
