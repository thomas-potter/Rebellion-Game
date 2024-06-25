using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    //pickup script for objects
    Pickup pickup;

    //Quest Manager
    [SerializeField]GameObject questManagerObject;
    QuestBase questScript;
    
    //Text for object pickup
    [SerializeField]GameObject PickupTextObject;

    TMP_Text pickupText;

    void Start()
    {
        questScript = questManagerObject.GetComponent<QuestBase>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.E) && inRangeOfNPC)
        {
            //check that there is a script referenced
            if (npcScript != null)
            {
                //run method on the other script
                npcScript.NPCTalk();
            }

        }

        if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.E) && inRangeOfItem)
        {
            //check that there is a script referenced
            if (pickup != null)
            {   
                Debug.Log("Test1");
                //run the item pickup method on quest and run it the item type
                questScript.ItemPickup(pickup.itemType);
                //turn of the text
                PickupTextObject.SetActive(false);
                //delete the picked up object
                pickup.DestroyPickupObject();
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
            
            //check if you can access the NPC Script
            if (npcScript != null)
            {
                if(!npcScript.currentlyTalking)
                {
                    npcScript.DisplayDefaulttext();
                }
                //Turn on the text
                npcScript.turnOnText(true);
                
            }
        }
        
        //for finding objects you want to pickup
        if(other.tag == "Pickup")
        {
            //Turn on the text 
            PickupTextObject.SetActive(true);
            //check if it has a pickup object
            pickup = other.GetComponent<Pickup>();
            //sets in range of item to true
            inRangeOfItem = true;

        }

    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "NPC")
        {
            inRangeOfNPC = false;
            //check if you can access the NPC Script
            if (npcScript != null)
            {
                //Turn off the text
                npcScript.turnOnText(false);
            }
            //Clear the NPC Script Reference
            npcScript = null;
        }

        if(other.tag == "Pickup")
        {
            PickupTextObject.SetActive(false);
            //in range of item
            inRangeOfItem = false;
            
            //Clear the Pickup Script Reference
            pickup = null;

        }
        
    }

}
