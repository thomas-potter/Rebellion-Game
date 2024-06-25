using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Movement")]
    [SerializeField] float moveSpeed;

    [SerializeField] float groundDrag;
    //Jump Variables
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    bool readyToJump = true;   

    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    bool isGrounded;


    [SerializeField] Transform orientation;

    float moveX = 0f;
    float moveY = 0f; 
    float jumpButton = 0f;

    Vector3 moveDirection;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        //Check if player is on the ground, get the player height and get half
        isGrounded = Physics.Raycast(transform.position,Vector3.down, playerHeight * 0.55f, whatIsGround );


        SpeedControl();

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
    }

    void ResetJump()
    {
        readyToJump = true;

    }

}
