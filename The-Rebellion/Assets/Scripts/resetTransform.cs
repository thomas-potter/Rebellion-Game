using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetTransform : MonoBehaviour
{
    float yPos;

    void Start()
    {
        //get the current position of the player in the begining
        //helps so its not hard coded into the code
        yPos = transform.localPosition.y;
    }

    void Update()
    {
        //set the player back to the middle to counteract the movement
        transform.localPosition = new Vector3(0f,yPos,0f);
    }
}
