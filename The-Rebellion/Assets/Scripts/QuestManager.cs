using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Quest Variables")]
    //[SerializeField] public int currentQuest;
    public bool[,] questStates;

    void Start()
    {
        //bool for the start and finsh of each quest 
        questStates = new bool[3,2];

        
    }

    public void ChangeArray(int Input1, int Input2, bool set)
    {   
        questStates[Input1,Input2] = set;

        
    }

    public void DebugArray(int Input1, int Input2)
    {   
        Debug.Log(questStates[Input1,Input2]);   
    }

    void Update()
    {
        //DebugArray(0,0);
    }



}
