using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Movement")]
    [SerializeField] float moveSpeed;
    int collisionCount = 0;

    [SerializeField] float groundDrag;
    //Jump Variables
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool readyToJump = true;   
    [SerializeField] Transform orientation;

    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    bool isGrounded;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] GameObject playerModel;


    [Header("Sound")]
    AudioSource audioSource1;
    [SerializeField] GameObject SecondAudioSourceObject;
    AudioSource audioSource2;



    

    float moveX = 0f;
    float moveY = 0f; 
    float jumpButton = 0f;
    float moveYRaw;
    float moveXRaw;

    Vector3 moveDirection;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource1 = GetComponent<AudioSource>();
        audioSource2 = SecondAudioSourceObject.GetComponent<AudioSource>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        
        GroundCheck();

        SpeedControl();

        PlayWalkSound();

        if(isGrounded)
        {
            rb.drag = groundDrag;
            
        }
        else
        {
            rb.drag = 0f;
        }

        MyInput();
    }

    void FixedUpdate()
    {
                
        PlayerMove();
    }

    void MyInput()
    {
        //Inputs for player movement
        moveX = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        moveY = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        //raw input without time.deltatime
        moveYRaw = Input.GetAxisRaw("Vertical");
        moveXRaw = Input.GetAxisRaw("Horizontal");

        //Input for jump
        jumpButton = Input.GetAxis("Jump");

        //To initiate jump if you're able to jump and you're grounded
        if(jumpButton > 0.1f && readyToJump && isGrounded)
        {
            //set to false so you can't imediatley jump
            readyToJump = false;

            Jump();
            //Set jump to true after a period of time
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void PlayerMove()
    {   
        //Getting the current direction of the player
        moveDirection = orientation.forward * moveY + orientation.right * moveX;

        //When on the ground
        if(isGrounded)
        {        
            //apply movement to the player in a certain direction
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);

        }

        else if(!isGrounded)
        {   
            //apply movement to the player in a certain direction with the air multiplier to change speed in the air
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
        }

        //set the variables to update in the animator
        animator.SetFloat("Forward", moveYRaw);
        animator.SetFloat("Sideways", moveXRaw);
        

        playerModel.transform.localEulerAngles = new Vector3(0f,90f * moveXRaw , 0f);
    }

    void SpeedControl()
    {
        //get the current move speed
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if you're moving faster then the move speed
        if(flatVelocity.magnitude > moveSpeed)
        {
            //take the current velocity and multiply it by the top speed
            Vector3 limitedVelocity = flatVelocity * moveSpeed;
            //apply new speed
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);

        }
    }

    void Jump() 
    {
        
        //reset y velocity to zero
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //apply force
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse );
        animator.Play("Jump", -1, 0f);
        //Play the jump sfx
        audioSource2.Play();
    }

    void ResetJump()
    {
        readyToJump = true;

    }

    void GroundCheck()
    {

        //Check if player is on the ground, get the player height and get half
        isGrounded = Physics.Raycast(transform.position,Vector3.down, playerHeight * 0.52f, whatIsGround );
        
        
        //If you're not in the air but still have the raycast as false
        if(!isGrounded && collisionCount > 0 )
        {
            //set the grounded to true
            isGrounded = true;
        }
        //if none are on
        else if(!isGrounded && collisionCount == 0 )
        {
            isGrounded = false;
        }
    }

    void PlayWalkSound()
    {
        if(isGrounded)
        {
            //play the sound if the player is moving
            if(audioSource1.isPlaying != true && moveYRaw > 0.5f || moveYRaw < -0.5f && audioSource1.isPlaying != true)
            {
                audioSource1.Play();
            }
            else if(audioSource1.isPlaying != true && moveXRaw > 0.5f || moveXRaw < -0.5f && audioSource1.isPlaying != true)
            {
                audioSource1.Play();
            }
            //Stop audio is player isn't moving
            else if (moveXRaw < 0.1f && moveXRaw > -0.1f && moveYRaw < 0.1f && moveYRaw > -0.1f)
            {
                if(audioSource1.isPlaying)
                {
                    audioSource1.Stop();
                }
            }
        }
        else 
        {
            //Stop the audio if not on the ground
            audioSource1.Stop();
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != "Pickup")
        {
            collisionCount++;
        }

    }
    
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag != "Pickup")
        {
            collisionCount--;
        }
    }

}
