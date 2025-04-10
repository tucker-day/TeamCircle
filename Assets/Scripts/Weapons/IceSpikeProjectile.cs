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
    }

    private void FixedUpdate()
    {
        if (SelectedEnemy != null)
        {
            Vector3 Target = SelectedEnemy.transform.position - transform.position;
            Vector3 NormalizedTarget = Vector3.Normalize(Target);
            transform.position = NormalizedTarget * speed * Time.fixedDeltaTime;
        }
        else
        {
            FindNearestEnemy();
        }

        if (Time.time > endTime)
        {
            Destroy(gameObject);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
