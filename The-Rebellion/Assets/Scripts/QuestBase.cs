
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestBase : MonoBehaviour
{
    [Header("Quest Variables")]
    [Range(0, 3)]
    public int currentQuest = 1;

    [Header("Current Quest State")]
    [SerializeField] bool started = false;
    public bool requirementsDone = false;
    [SerializeField] bool finished = false;

    [Header("Quest 1")]
    [SerializeField] GameObject quest1Items;
    [SerializeField]int currentPickupCountQ1 = 0;
    [SerializeField]int maxPickupCountQ1 = 5;

    [Header("Quest 2")]
    [SerializeField] GameObject quest2Items;
    [SerializeField]int currentPickupCountQ2 = 0;
    [SerializeField]int maxPickupCountQ2 = 3;

    [Header("Quest 3")]
    [SerializeField] GameObject quest3Items;
    [SerializeField]int currentPickupCountQ3 = 0;
    [SerializeField]int maxPickupCountQ3 = 3;

    QuestManager questManager;
    QuestTextManager questTextManager;


    void Start()
    {
        questManager = GetComponent<QuestManager>();
        questTextManager = GetComponent<QuestTextManager>();

        //Make sure all the items for the quests are invisible at start
        quest1Items.SetActive(false);
        quest2Items.SetActive(false);
        quest3Items.SetActive(false);

    }

    void Update()
    {
        Quest1();
        Quest2();
        Quest3();
    }

    public void StartQuest()
    {
        if(questManager.questStates[currentQuest -1, 0] == false)
        {
            //Set Current Quest to True on Quest Manager Script
            questManager.ChangeArray(currentQuest-1, 0, true);

            //Turn on the text
            questTextManager.EnableText(true);

            //Set Quest Started variable to true
            started = questManager.questStates[currentQuest -1, 0];  
        }
    }

    public void QuestRequirementsFinished()
    {
        //Check if requirements are done
        if(questManager.questStates[currentQuest -1, 1] == false && started)
        {
            requirementsDone = true;
        }
    }

    public void CompleteQuest()
    {

        if(started && requirementsDone)
        {
            Debug.Log("Finish Quest");
            //turn of text
            questTextManager.EnableText(false);
            //Set Finished to true
            questManager.ChangeArray(currentQuest-1, 1, true);
            //Reset All the variables for the quest
            ResetVariables();
            //Go to the next Quest
            currentQuest += 1;            
        }     
    }

    void ResetVariables()
    {
        started = false;
        requirementsDone = false;
        finished = false;
    }

    public void ItemPickup(int itemType)
    {   
        //if the item type is the same as the quest add to the count
        if(currentQuest == itemType)
        {
            if(currentQuest == 1)
            {
                currentPickupCountQ1 ++;
            }
            if(currentQuest == 2)
            {
                currentPickupCountQ2 ++;
            }
            if(currentQuest == 3)
            {
                currentPickupCountQ3 ++;
            }
        
        
        }
    }

    void Quest1()
    {   
        //Check you're on the right script
        if(currentQuest == 1 && started)
        {
            
            //Check to see if the quest items are active
            if(!quest1Items.activeInHierarchy)
            {
                questTextManager.SlashText(false);
                //set them active
                quest1Items.SetActive(true);
            }
            
            //Write the quest text
            questTextManager.WriteText("Find Milk Bottles (" + currentPickupCountQ1 + "/" + maxPickupCountQ1 + ")");
            //if you've pixked up all the items slash the text
            if (currentPickupCountQ1 >= maxPickupCountQ1)
            {
                requirementsDone = true;
                questTextManager.SlashText(true);

            }
        }
    }

    void Quest2()
    {
        if(currentQuest == 2 && started)
        {

            //Check to see if the quest items are active
            if(!quest2Items.activeInHierarchy)
            {
                questTextManager.SlashText(false);
                //set them active
                quest2Items.SetActive(true); 
            }
            
            //Write the quest text
            questTextManager.WriteText("Find Photos (" + currentPickupCountQ2 + "/" + maxPickupCountQ2 + ")");
            //if you've pixked up all the items slash the text
            if (currentPickupCountQ2 >= maxPickupCountQ2)
            {
                requirementsDone = true;
                questTextManager.SlashText(true);
            }
        }
    }

    void Quest3()
    {
        if(currentQuest == 3 && started)
        {

            //Check to see if the quest items are active
            if(!quest3Items.activeInHierarchy)
            {
                questTextManager.SlashText(false);
                //set them active
                quest3Items.SetActive(true);
            }

            //Write the quest text
            questTextManager.WriteText("Find Footballs (" + currentPickupCountQ3 + "/" + maxPickupCountQ3 + ")");
            //if you've pixked up all the items slash the text
            if (currentPickupCountQ3 >= maxPickupCountQ3)
            {
                requirementsDone = true;
                questTextManager.SlashText(true);

            }

        }

    }

}
