//Author: Kim Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private TMP_Text speedValue,
        volumeValue;

    [SerializeField]
    private Slider textSlider,
        volumeSlider;

    void Awake()
    {
        //Check if there are no player settings yet
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            //Set up base settings
            PlayerPrefs.SetFloat("MasterVolume", 0.2f);
            PlayerPrefs.SetFloat("TextSpeed", 2f);
            PlayerPrefs.Save();
        }
        //Get settings and set state of sliders
        volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        AudioListener.volume = volumeSlider.value;
        volumeValue.SetText((int)(volumeSlider.value * 100) + "%");

        textSlider.value = PlayerPrefs.GetFloat("TextSpeed");
        speedValue.SetText(((double)(textSlider.value)).ToString());
    }

    //called by settings-button
    public void ToggleSettings()
    {
        //toggle settings
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    //Called by value-changes of the text speed slider
    public void ChangeTextSpeed()
    {
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

    //Called by StartGame button
    public void StartGame()
    {
        //Loads the level
        SceneManager.LoadScene("Main");
    }

    //Called by ExitGame button
    public void Exit()
    {
        //Quit game
        Application.Quit();
    }
}
