using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject minimapCameraPrefab;
    private Camera minimapCamera;
    private GameObject player;

    private bool bigMapDisplay = false;
    [SerializeField]
    private GameObject minimap;
    [SerializeField]
    private GameObject biggymap;

    const float MINIMAP_SIZE = 75;
    const float BIGGYMAP_SIZE = 175;

    // Start is called before the first frame update
    void Start()
    {
        minimapCamera = Instantiate(minimapCameraPrefab).GetComponent<Camera>();
        minimapCamera.orthographicSize = MINIMAP_SIZE;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log(player.transform.position.x);
        minimapCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, minimapCamera.transform.position.z);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bigMapDisplay = !bigMapDisplay;
            minimap.SetActive(!bigMapDisplay);
            biggymap.SetActive(bigMapDisplay);
            minimapCamera.orthographicSize = (bigMapDisplay) ? BIGGYMAP_SIZE : MINIMAP_SIZE;
        }
    }
}
