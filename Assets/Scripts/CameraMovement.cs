using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    private float offsetX;
    private float offsetY = -2f;
    private float offsetZ = -10f;

    private float cameraLeftLimit;
    private float cameraInitial;
    void Start()
    {
        cameraLeftLimit=player.transform.position.x;
        cameraInitial = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraLeftLimit<player.transform.position.x&& cameraInitial<player.transform.position.x)
            {
            transform.position = new Vector3((player.transform.position.x - offsetX),transform.position.y , offsetZ);
            }
        transform.position = new Vector3(transform.position.x,(player.transform.position.y - offsetY), offsetZ);
        }
}
