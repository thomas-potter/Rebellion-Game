using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestTextManager : MonoBehaviour
{
    [SerializeField]GameObject QuestTextObject;

    TMP_Text questText;

    void Start()
    {
        questText = QuestTextObject.GetComponent<TMP_Text>();

    }

    //Turn the text on an off
    public void EnableText(bool textOn)
    {
        QuestTextObject.SetActive(textOn);
    }

    //use this to write the text
    public void WriteText(string questTextUpdate)
    {
        questText.text = questTextUpdate;

    }

    //use this to strikethrough the text
    public void SlashText(bool isTrue)
    {
        if(isTrue)
        {
            questText.fontStyle = FontStyles.Strikethrough; 
        }
        if(!isTrue)
        {
            questText.fontStyle = FontStyles.Normal; 
        }


    }
}
