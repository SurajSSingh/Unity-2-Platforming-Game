using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        // Assign if it is not yet assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        // If player is not null, then get offset
        if (player != null)
        {
            offset = this.transform.position - player.transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Update position of the camera based on player movement
        this.transform.position = player.transform.position + offset;
    }
}
