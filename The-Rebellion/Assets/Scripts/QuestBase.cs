using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestBase : MonoBehaviour
{
    bool readyToStart = false;
    bool started = false;
    bool finished = false;

    [SerializeField] TextMeshPro TMP;

    void Start()
    {
        TMP = GetComponent<TextMeshPro>();
    }

    public virtual void StartQuest()
    {
        if(started = false && finished = false)
        {
            started = true;
        }
    }

    public virtual void UpdateQuest(int questType)
    {
        if(started = true && finished = false)
        {
            
        }
    }

    public virtual void CompleteQuest()
    {
        if(started = true && finished  = false)
        {

        }
    }


}
