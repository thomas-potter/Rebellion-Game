using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField]  Transform orientation;
    [SerializeField]  Transform player;
    [SerializeField]  Transform playerObject;
    [SerializeField]  Transform lookAt;
    [SerializeField] Rigidbody rb;

    [SerializeField] int cameraStyle = 2;
    
    [SerializeField] float rotationSpeed;

    void Start()
    {
        //Lock the mouse and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        if(cameraStyle == 1)
        {
            Debug.Log("1");
            //get the orientation gameobject to follow the current camera rotation
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;

            //Get player inpiut
            float moveX = Input.GetAxis("Horizontal") * Time.deltaTime;
            float moveY = Input.GetAxis("Vertical") * Time.deltaTime;

            //Get how much you want the player to rotate based on the player input
            Vector3 inputDir = orientation.forward * moveY + orientation.right * moveX;

            //Check to see that current rotation is not 0
            if(inputDir != Vector3.zero)
            {   
                //make the current player facing forward with the input direction and how fast to rotate it
                //Slerp is used to interpolate between two vectors, first value is current player rotation, second value is desired rotation
                playerObject.forward = Vector3.Slerp(playerObject.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);    

            }
        }

        if(cameraStyle == 2)
        {
            
            Vector3 dirToLookAt = lookAt.position - new Vector3(transform.position.x, lookAt.position.y, transform.position.z);
            orientation.forward = dirToLookAt.normalized;

            playerObject.forward = dirToLookAt.normalized;

        }
    }
}
