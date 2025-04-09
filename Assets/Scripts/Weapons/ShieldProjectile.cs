using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CircleCollider2D))]
public class ShieldProjectile : MonoBehaviour
{
    const float LIFETIME = 5.0f;

    [HideInInspector]
    public int damage;
    
    public float speed;

    public float endTime;
    [SerializeField]
    int rotationSpeed;

    [SerializeField]
    GameObject Square;

    private Enemy SelectedEnemy;
    public void Start()
    {
        endTime = Time.time + LIFETIME;
        FindNearestEnemy();
    }

    private void FixedUpdate()
    {
        //transform.position += speed * Time.fixedDeltaTime * transform.right;

        //find distance vector between closest enemy and shield
        //then normalize distance vector
        //multipy the distance vector by speed

        Vector3 Target = SelectedEnemy.transform.position- transform.position;
        Vector3 NormalizedTarget = Vector3.Normalize(Target);
        transform.position += NormalizedTarget * speed * Time.fixedDeltaTime;

        Square.transform.localRotation = Quaternion.Euler(0f, 0f, Square.transform.localRotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));

        if (Time.time > endTime)
        {
            Destroy(gameObject);
        }
    }

    private void FindNearestEnemy()
    {
        float ClosestDistance = 120012f;

        foreach (Enemy enemy in Enemy.s_enemyList)
        {
            Debug.Log(enemy);
            float CurrentDistance = Vector3.Distance(enemy.transform.position, transform.position);
            if (CurrentDistance < ClosestDistance)
            {
                ClosestDistance = CurrentDistance;
                SelectedEnemy = enemy;
            }

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


//if hits > level then destroy else, increase hit counter then find next enemy