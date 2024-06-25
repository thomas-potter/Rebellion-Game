using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [Header("Raycast Var")]
    [SerializeField] Transform cameraTransform;
    //use this to stop the raycast hitting the player
    [SerializeField] LayerMask pickupLayerMask;

    [Header("Trigger Variables")]
    [SerializeField] bool inRangeOfNPC = false;
    [SerializeField] bool inRangeOfItem = false;

    //The npc interaction script
    NPCInteraction npcScript;


    void Update()
    {
        if(Input.GetAxis("Interact") > 0.1f && inRangeOfNPC)
        {
            //check that there is a script referenced
            if (npcScript != null)
            {
                //run method on the other script
                npcScript.NPCTalk();
            }
        }

    }

    //In range with a distance style of interacting with NPCs and Objects
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            Debug.Log("In Range");
            inRangeOfNPC = true;

            //try get a script from the other object
            npcScript = other.GetComponent<NPCInteraction>();
        }

    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "NPC")
        {
            Debug.Log("Out of Range");
            inRangeOfNPC = false;

            npcScript = null;
        }
        
    }
    


    //Raycast style of interacting with objects and NPCS
    void RayCastSettup()
    {
        if(Input.GetAxis("Interact") > 0.1f)
        {
            //check that you haven't picked anything up
            if(npcScript == null)
            {
                float pickupDistance = 20f;
                //Send out a ray from the camera in a straight line - set max distance - apply the layer mask to stop hitting the player
                if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit raycastHit, pickupDistance,pickupLayerMask ))
                {
                    //try find the script on the object that the raycast hits
                    if(raycastHit.transform.TryGetComponent(out npcScript))
                    {
                        Debug.Log("Hit");
                        //Run whatever code on the other script

                    }

                }
            }

            else
            {
                npcScript = null;
            }
        
        }
    }
}
