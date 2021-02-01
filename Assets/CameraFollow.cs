using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;

    // Start is called before the first frame update


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(0, player.transform.position.y+4, -40);
    }
}
