using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class IceSpikeProjectile : MonoBehaviour
{
    const float LIFETIME = 5.0f;

    [HideInInspector]
    public int damage;

    public float speed;

    public float endTime;
    [SerializeField]
    int rotationSpeed;

    [SerializeField]
    GameObject IceSpikeSquare;

    private GameObject SelectedEnemy;


    public void Start()
    {
        endTime = Time.time + LIFETIME;
        FindNearestEnemy();
        
        if (SelectedEnemy != null)
        {
            Vector3 Target = SelectedEnemy.transform.position;
            transform.position = Target;
            SelectedEnemy.GetComponent<Enemy>().Freeze();
            SelectedEnemy.GetComponent<Enemy>().TakeDamage(damage);
        }

        Destroy(gameObject, 3.0f);
    }

    private void FindNearestEnemy()
    {
        float ClosestDistance = 120012f;
        GameObject tempEnemy = null;

        if (Enemy.s_enemyList.Count <= 0)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        foreach (Enemy enemy in Enemy.s_enemyList)
        {
            float CurrentDistance = Vector3.Distance(enemy.transform.position, transform.position);

            if (CurrentDistance < ClosestDistance && enemy.gameObject != SelectedEnemy)
            {
                ClosestDistance = CurrentDistance;
                tempEnemy = enemy.gameObject;
            }
        }

        if (tempEnemy != null)
        {
            SelectedEnemy = tempEnemy;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
