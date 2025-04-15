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

    public float endTime;

    [SerializeField]
    GameObject IceSpikeSquare;

    public void Update()
    {

    }


}
