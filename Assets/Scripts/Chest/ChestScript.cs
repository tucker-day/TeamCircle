using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour

{
    public GameObject[] weapons; 
    public GameObject chestPrefab; 
    public Vector3 chestPosition; 
    public float animationDuration = 1f;
    private bool isChestSpawned = false; 
    private GameObject chestInstance;

    private void Start()
    {
        chestPrefab.SetActive(false); 
    }

    private void Update()
    {
       
        if (!isChestSpawned && AreAllEnemiesDefeated())
        {
            SpawnChest();
        }
    }

    private bool AreAllEnemiesDefeated()
    {
       
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            if (enemy.isAlive)
            {
                return false; 
            }
        }
        return true; 
    }

    private void SpawnChest()
    {
        isChestSpawned = true;

        chestInstance = Instantiate(chestPrefab, chestPosition, Quaternion.identity);
        chestInstance.SetActive(true);

        
        StartCoroutine(OpenChest());
    }

    private IEnumerator OpenChest()
    {
        
        Vector3 initialScale = chestInstance.transform.localScale;
        Vector3 targetScale = new Vector3(1f, 1f, 1f); 
        float timeElapsed = 0f;

        while (timeElapsed < animationDuration)
        {
            chestInstance.transform.localScale = Vector3.Lerp(initialScale, targetScale, timeElapsed / animationDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        chestInstance.transform.localScale = targetScale;

       
        DropRandomWeapon();
    }

    private void DropRandomWeapon()
    {
       
        int randomIndex = Random.Range(0, weapons.Length);
        Instantiate(weapons[randomIndex], chestInstance.transform.position, Quaternion.identity);
    }
}