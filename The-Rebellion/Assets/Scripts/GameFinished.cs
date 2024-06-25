using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{

    QuestManager questMangerScript;

    void Start()
    {
        questMangerScript = GetComponent<QuestManager>();
    }
    // Update is called once per frame
    void Update()
    {
        bool q1Finished = questMangerScript.questStates[0, 1];
        bool q2Finished = questMangerScript.questStates[1, 1];
        bool q3Finished = questMangerScript.questStates[2, 1];
        
        if(q1Finished && q2Finished && q3Finished)
        {
            SceneManager.LoadScene(sceneName: "GameOver");
        }
    }
}
