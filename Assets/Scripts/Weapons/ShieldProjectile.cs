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

    private GameObject SelectedEnemy;
    public int BounceCount;
    private Shield shield;

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
            transform.position += NormalizedTarget * speed * Time.fixedDeltaTime;
        }
        else {
            FindNearestEnemy();
        }
        Square.transform.localRotation = Quaternion.Euler(0f, 0f, Square.transform.localRotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));

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
                Debug.Log("Replacing " + SelectedEnemy + " with " + enemy.gameObject + " with distance " + CurrentDistance);
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
            BounceCount--;
            if (BounceCount <= 0)
            {
                Destroy(gameObject);
            }
            else 
            { 
                FindNearestEnemy();
            }
        }
    }
}


//if hits > level then destroy else, increase hit counter then find next enemy
//save current target, then if its called again then ignore it