using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GhostHunter : MonoBehaviour
{
    public GameObject ghost;

    private Rigidbody rigidbodyPlayer;
    private Animator playerAnimator;

    private float horizontalInput;
    private float verticalInput;

    public int walkSpeed;
    public int jumpSpeed;

    private bool isGrounded = true;

    public static int lifePlayer = 10;
    public int points = 100;

    private Collider capsuleCollider;
    public Collider boxCollider;
    bool killIt;
    
    void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
       
    }


    void Update()
    {


    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        isGrounded = true; 

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            playerAnimator.SetBool("idleParam", false);
            playerAnimator.SetBool("walkParam", false);
            playerAnimator.SetBool("jumpP", true);
            //playerAnimator.Play("Jumping");
            rigidbodyPlayer.MovePosition(rigidbodyPlayer.position + new Vector3(0, jumpSpeed, 0));

        }
        else { 
            playerAnimator.SetBool("jumpP", false); 
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            killIt = true;
            playerAnimator.SetBool("walkParam", false);
            playerAnimator.SetBool("idleParam", false);
            playerAnimator.SetTrigger("killParam");
            //playerAnimator.Play("Kill"); 
            
        } 

        if (horizontalInput != 0)
        {
            playerAnimator.SetBool("idleParam", false);
            playerAnimator.SetBool("walkParam", true);
            playerAnimator.Play("Walk");
           // rigidbodyPlayer.MovePosition(rigidbodyPlayer.position+ new Vector3(horizontalInput * walkSpeed, 0, 0));


            if (horizontalInput < 0)
            {
                transform.localEulerAngles = new Vector3(0, -90, 0);
                rigidbodyPlayer.MovePosition(rigidbodyPlayer.position + new Vector3(horizontalInput, 0f, 0f) * Time.deltaTime * walkSpeed);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 90, 0);
                rigidbodyPlayer.MovePosition(rigidbodyPlayer.position + new Vector3(horizontalInput, 0f, 0f) * Time.deltaTime * walkSpeed);
            }

        }
        else
        {
            playerAnimator.SetBool("walkParam", false);
        }

        if (verticalInput != 0)
        {
            playerAnimator.SetBool("idleParam", false);
            playerAnimator.SetBool("walkParam", true);
            //playerAnimator.Play("Walk");

            if (verticalInput < 0)
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
                rigidbodyPlayer.MovePosition(rigidbodyPlayer.position + new Vector3(0f, 0f, verticalInput) * Time.deltaTime * walkSpeed);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                rigidbodyPlayer.MovePosition(rigidbodyPlayer.position + new Vector3(0f, 0f, verticalInput) * Time.deltaTime * walkSpeed);
            }
        }
        else
        {
            playerAnimator.SetBool("walkParam", false);
        }

        if (isGrounded && verticalInput==0 && horizontalInput ==0)
        {
            playerAnimator.SetBool("idleParam", true);
            //playerAnimator.Play("Idlе");
        } 
    }

   

    private void OnCollisionEnter(Collision other)
    {
    if (other.gameObject.tag == "Enemy" && killIt)
    {
        GameManager.Instance.Score += points;
        Destroy(other.gameObject); 
    } 
    

    if (other.gameObject.tag == "Enemy" && !killIt)
    {
        lifePlayer--;
        GameManager.Instance.Life = lifePlayer;
        Destroy(other.gameObject); 
    }


    if (other.gameObject.tag == "Cube")
        {
            isGrounded = true;
        }
    }


    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Cube" && isGrounded)
        {
            isGrounded = false;
        }

        killIt = false;
    } 
}
