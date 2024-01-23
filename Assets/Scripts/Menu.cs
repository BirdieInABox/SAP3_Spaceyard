using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu,
        settingsMenu,
        bg;

    [SerializeField]
    private Slider textSlider,
        volumeSlider;

    [SerializeField]
    private Dialogue dialogue;

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeTextSpeed()
    {
        dialogue.ChangeSpeed(textSlider.value);
    }

    public void ChangeMasterVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void OnToggle(InputValue value)
    {
        if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            bg.SetActive(false);
            mainMenu.SetActive(false);
        }
        else
            ToggleMenu();
    }

    public void Resume()
    {
        ToggleMenu();
    }

    public void ToggleMenu()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        bg.SetActive(!bg.activeSelf);
        ToggleCursor();
    }

    private void ToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = !Cursor.visible;
    }

    public void ToggleSettings()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }
}