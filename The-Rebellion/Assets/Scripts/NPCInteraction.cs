using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [Header("NPC Quest Number")]
    [Range(1,3)]
    [SerializeField] int npcQuestNumber;

    [Header("Quest Text Manager")]
    [SerializeField] string defaultTextToDisplay;
    [SerializeField] string goAwayText;
    [SerializeField] string[] dialogText; 
    [SerializeField] int curTextIndex = -1; 
    [SerializeField]int questBreakLevel = 0;
    public bool currentlyTalking = false;

    [Header("References")]
    [SerializeField]GameObject npcTextObject;
    TMP_Text npcText;

    [SerializeField]GameObject questManagerObject;
    QuestBase questScript;
    QuestManager questManager;
    
    int currentQuest;

    void Start()
    {
        npcText = npcTextObject.GetComponent<TMP_Text>();
        questScript = questManagerObject.GetComponent<QuestBase>();
        questManager = questManagerObject.GetComponent<QuestManager>();
    }

    void Update()
    {
        //if the quest is done and above the break point
        if(questScript.requirementsDone && curTextIndex < questBreakLevel +1 &&  questScript.currentQuest == npcQuestNumber)
        {
            curTextIndex +=1;  
            //Write Text
            WriteText(dialogText[curTextIndex]);
        }

        currentQuest = questScript.currentQuest;

    }
    //array for NPC
    public void turnOnText(bool turnOn)
    {
        npcTextObject.SetActive(turnOn);

    }

    public void DisplayDefaulttext()
    {
        //if you're on the right quest
        if(questScript.currentQuest == npcQuestNumber)
        {
            WriteText(defaultTextToDisplay);
        }
        //if on the wrong quest tell them to go away
        else
        {
            npcText.text = goAwayText;
        }

    }
    //this gets run to talk to the NPC
    public void NPCTalk()
    {   
        //check the current working quest is the one the npc wants
        if(questScript.currentQuest == npcQuestNumber)
        {
            //set this to acctualy start talking
            currentlyTalking = true;

            //quest break level is where you go complete the quest 
            //set it so you can interact if you're above or below that level
            if(curTextIndex < questBreakLevel || curTextIndex > questBreakLevel && curTextIndex < dialogText.Length -1)
            {
                curTextIndex +=1;  
                //write the text
                WriteText(dialogText[curTextIndex]);
            }
            //if you're currently in the quest break level
            //run the quest
            else if(curTextIndex == questBreakLevel)
            {
                questScript.StartQuest();
                npcTextObject.SetActive(false);

            }
            //If you're on the text piece
            if(curTextIndex == dialogText.Length -1)
            {   
                //complete the quest
                questScript.CompleteQuest();
                //add one more so you can know when to clear the text
                curTextIndex +=1;
            }
        }
        if(curTextIndex == dialogText.Length)
        {
            npcText.text = "";
        }
    }

    void WriteText(string text1)
    {

        npcText.text = text1 + " (press E to continue)";

        
        
    }
}
