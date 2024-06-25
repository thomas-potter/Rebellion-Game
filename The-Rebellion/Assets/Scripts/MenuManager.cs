using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] bool inMenu;
    

    [Header("Pause")]
    [SerializeField] GameObject pauseMenu;
    public bool isPaused;

    [Header("Settings")]
    [SerializeField] GameObject settingsMenu;
    [SerializeField] bool inSettings;

    [Header("Selection")]
    [SerializeField] GameObject pauseFirstButton;
    [SerializeField] GameObject settingsFirstButton;
    [SerializeField] GameObject settingsCloseButton;

    // Start is called before the first frame update
    void Start()
    {
        if(isPaused != null)
        {
            pauseMenu.SetActive(false);
        }
        settingsMenu.SetActive(false);

        cursorLocked(true);
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if you're in the main menu
        if(!inMenu)
        { 
            //Check for escape Key on keyboard and Start button on controller
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                //check to see if game is already paused
                if(!isPaused)
                {
                    //pause + unlock cursor
                    PauseGame();
                    cursorLocked(false);
                }
               //check if you're on normal pause menu
                else if(isPaused && !inSettings)
                {
                    //resume game and lock cursor
                    ResumeGame();
                    cursorLocked(true);
                }
                //if you're in the settings menu
                else if(isPaused && inSettings)
                {
                    CloseSettings();
                }
            }
        }
        else if(inMenu && inSettings)
        {
            //if you press escape or B button
            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                CloseSettings();
            }
        }
        if(inMenu)
        {
            cursorLocked(false);
        }

    }

    public void StartGame()
    {
        //Load the Main Level
        SceneManager.LoadScene(sceneName: "Level");

        inMenu = false;
    }

    public void PauseGame()
    {
        Debug.Log("Pause");
        //Set the UI to true
        pauseMenu.SetActive(true);
        //Pause time
        Time.timeScale = 0f;
        //Bool for being paused
        isPaused = true;

        //Selection for controller
        //clear interaction selection
        EventSystem.current.SetSelectedGameObject(null);
        //Select the first object
        EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void ResumeGame()
    {
        Debug.Log("Resume");
        //Remove the UI
        pauseMenu.SetActive(false);
        //Start Time
        Time.timeScale = 1f;    
        //Set Bool
        isPaused = false;
    }

    public void RunSettings()
    {
        Debug.Log("Open Settings");
        //turn off pause UI and add Settings 
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        //Set Bool
        inSettings = true;

        //Selection for controller
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
        
    }

    public void CloseSettings()
    {   
        Debug.Log("Close Settings");
        //turn off Settings UI and turn on pause Menu 
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);

        inSettings = false;

        //Selection for controller
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsCloseButton);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    void cursorLocked(bool isTrue)
    {
        if(!isTrue)
        {
            //Lock the mouse and make it invisible
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

        }
        if(isTrue)
        {
            //Lock the mouse and make it invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
        
    }
}
