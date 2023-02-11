using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Ghost : MonoBehaviour
{
   
    private Animator ghostAnimator;
    Vector3 ghostRandomPos;

    private Transform player;
    public float maxDistanceZ;
    public float maxDistanceX;
    private Rigidbody ghostRigidbody;

    private float distanceX;
    private float distanceZ;
    private Vector3 posToMove;
    private float ghostSpeed = 0.1f;

   
    void Start()
    {
        ghostAnimator = GetComponent<Animator>();
        ghostRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;
    }

   
    void Update()
    {
        ghostAnimator.Play("Armature|Ghost Walk");
    }

    private void FixedUpdate()
    {
       
        distanceX = System.Math.Abs(transform.position.x - player.transform.position.x);
        distanceZ = System.Math.Abs(transform.position.z - player.transform.position.z);

        
            Vector3 direction = player.transform.position - transform.position; 
            transform.LookAt(player);
            ghostRigidbody.MovePosition(transform.position + (direction  * ghostSpeed * Time.deltaTime));
      
    }
}
    