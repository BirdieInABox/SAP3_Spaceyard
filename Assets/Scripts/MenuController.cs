//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    //SubMenus
    [SerializeField]
    private GameObject mainMenu,
        settingsMenu,
        bg;

    //Sliders
    [SerializeField]
    private Slider textSlider,
        volumeSlider;

    //Dialoguesystem
    [SerializeField]
    private DialogueSystem dialogue;

    //Percentage values for sliders
    [SerializeField]
    private TMP_Text speedValue,
        volumeValue;

    [SerializeField]
    private PlayerController player;

    //Called by ExitGame button
    public void Exit()
    {
        //Quit game
        Application.Quit();
    }

    void Awake()
    {
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            PlayerPrefs.SetFloat("MasterVolume", 0.2f);
            PlayerPrefs.SetFloat("TextSpeed", 2f);
            PlayerPrefs.Save();
        }
        //Set base state of sliders
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        AudioListener.volume = volumeSlider.value;
        volumeValue.SetText((int)(volumeSlider.value * 100) + "%");

        textSlider.value = PlayerPrefs.GetFloat("TextSpeed");
        dialogue.ChangeSpeed(1 / (textSlider.value * 10));
        speedValue.SetText(((double)(textSlider.value)).ToString());
    }

    //Called by value-changes of the text speed slider
    public void ChangeTextSpeed()
    {
        //Set text speed in DialogueSystem
        dialogue.ChangeSpeed(1 / (textSlider.value * 10));
        //Change text to represent the current slider value
        speedValue.SetText(((double)(textSlider.value)).ToString());
        PlayerPrefs.SetFloat("TextSpeed", textSlider.value);
        PlayerPrefs.Save();
    }

    //Called by value-changes of the master volume slider
    public void ChangeMasterVolume()
    {
        //Set volume to current slider value
        AudioListener.volume = volumeSlider.value;
        //Change text to represent the current slider value
        volumeValue.SetText((int)(volumeSlider.value * 100) + "%");
        PlayerPrefs.SetFloat("MasterVolume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    //Called on press of Escape-Key
    public void OnToggle(InputValue value)
    {
        //if settings are open
        if (settingsMenu.activeSelf)
        {
            Time.timeScale = 1;
            player.stopMovement = !player.stopMovement;
            //deactivate all menus manually
            settingsMenu.SetActive(false);
            bg.SetActive(false);
            mainMenu.SetActive(false);
            ToggleCursor();
        }
        else
            ToggleMenu();
    }

    //Called by Resume-button
    public void Resume()
    {
        ToggleMenu();
    }

    //Turns on/off menu
    public void ToggleMenu()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
        //toggle menu object
        mainMenu.SetActive(!mainMenu.activeSelf);
        //toggle menu background
        bg.SetActive(!bg.activeSelf);
        //toggle player movement
        player.stopMovement = !player.stopMovement;
        //toggle hotbar scrollability
        player.inventory.hotbar.gameObject.GetComponent<PlayerInput>().enabled =
            !player.inventory.hotbar.gameObject.GetComponent<PlayerInput>().enabled;
        //toggle cursor
        ToggleCursor();
    }

    //Turns on/off cursor and lock/unlocks it
    private void ToggleCursor()
    {
        //unlock if locked
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else //lock if unlocked
            Cursor.lockState = CursorLockMode.Locked;
        //toggle cursor visibility
        Cursor.visible = !Cursor.visible;
    }

    //called by settings-button
    public void ToggleSettings()
    {
        //toggle menu
        mainMenu.SetActive(!mainMenu.activeSelf);
        //toggle settings
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
