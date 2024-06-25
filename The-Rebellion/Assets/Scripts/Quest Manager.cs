using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    bool[,] questStates;

    void Start()
    {
        //bool for the start and finsh of each quest 
        questStates = new bool[3,2];
    }



}
