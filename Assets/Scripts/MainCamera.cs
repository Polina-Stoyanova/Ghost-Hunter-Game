using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 oldPos;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Awake()
    {
        oldPos = player.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        if (playerPos != oldPos)
        {
            oldPos = playerPos; 
            transform.position = new Vector3(playerPos.x, 13, playerPos.z - 17f);
        }
       
    }
}
