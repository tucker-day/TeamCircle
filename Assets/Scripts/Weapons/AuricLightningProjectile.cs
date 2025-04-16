using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AuricLightningProjectile : MonoBehaviour
{
    const float LIFETIME = 5.0f;

    [HideInInspector]
    public int damage;

    public float speed;

    public float endTime = 0.0f;

    [SerializeField]
    GameObject IceSpikeSquare;

    public void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            endTime -= Time.deltaTime;
            if (endTime <= 0.0f) {
                endTime = 1.0f;
                Debug.Log("lightning attack");
                enemy.TakeDamage(damage);
            }
        }
    }
}
