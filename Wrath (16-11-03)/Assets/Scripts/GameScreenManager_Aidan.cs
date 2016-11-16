using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScreenManager_Aidan : MonoBehaviour {

    public RectTransform mainMenu;
    public RectTransform settingsMenu;
    public Toggle fullscreenToggle;
    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    public int[] screenWidths;
    public bool MainMenu = true;

    int activeScreenResIndex = 0;

    void Start()
    {
        if (MainMenu)
        {
            activeScreenResIndex = PlayerPrefs.GetInt("screenResolutionIndex", activeScreenResIndex);
            bool isFullscreen = ((PlayerPrefs.GetInt("fullscreen")) == 1) ? true : false;
            volumeSliders[0].value = AudioManager_Aidan.instance.masterVolumePercent;
            volumeSliders[1].value = AudioManager_Aidan.instance.musicVolumePercent;
            volumeSliders[2].value = AudioManager_Aidan.instance.sfxVolumePercent;

            for (int i = 0; i < resolutionToggles.Length; i++)
            {
                resolutionToggles[i].isOn = (i == activeScreenResIndex);
            }

            if (isFullscreen)
            {
                fullscreenToggle.isOn = isFullscreen;
                Resolution[] allResolutions = Screen.resolutions;
                Resolution maxResolution = allResolutions[allResolutions.Length - 1];
                Screen.SetResolution(maxResolution.width, maxResolution.height, true);
            }
        }
    }

	public void PlayAgain()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        MainMenu = false;
        GameManager_Aidan.instance.isInGameScene = true;
        SceneManager.LoadScene("aidan_working_scene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MainMenu = true;
        GameManager_Aidan.instance.isInGameScene = false;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void SettingsMenu()
    {
        mainMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        settingsMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16f / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt("screenResolutionIndex", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetFullScreen(bool isFullscreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }

        PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float value)
    {
        if (AudioManager_Aidan.instance != null)
        {
            AudioManager_Aidan.instance.SetVolume(value, AudioManager_Aidan.AudioChannel.Master);
        }
    }

    public void SetSFXVolume(float value)
    {
        if (AudioManager_Aidan.instance != null)
        {
            AudioManager_Aidan.instance.SetVolume(value, AudioManager_Aidan.AudioChannel.SFX);
        }
    }

    public void SetMusicVolume(float value)
    {
        if (AudioManager_Aidan.instance != null)
        {
            AudioManager_Aidan.instance.SetVolume(value, AudioManager_Aidan.AudioChannel.Music);
        }
    }
}
