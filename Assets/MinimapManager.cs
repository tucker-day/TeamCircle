using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject minimapCameraPrefab;
    private GameObject minimapCamera;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        minimapCamera = Instantiate(minimapCameraPrefab);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(player.transform.position.x);
        minimapCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, minimapCamera.transform.position.z);
    }
}
