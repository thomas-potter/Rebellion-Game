using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TextButton : MonoBehaviour
{
    TMP_Text buttonText;
    [Header("Font Scaling")]
    [SerializeField] float fontNormalSize;
    [SerializeField] float fontScaledSize;
    [SerializeField] float scaleMultiplier;

    void Start()
    {
        buttonText = GetComponent<TMP_Text>();

        //Get the current font size of the text
        fontNormalSize = buttonText.fontSize;
        //scaled size is set with a multiplier
        fontScaledSize = fontNormalSize * scaleMultiplier; 
    }

    public void PointEnter()
    {
        //Set the font to scaled sized
        buttonText.fontSize = fontScaledSize;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameObject);
        
    }

    public void PointExit()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //Set the font size back to default
        buttonText.fontSize = fontNormalSize;
    }

}
