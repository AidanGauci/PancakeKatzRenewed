using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameScreenManager_Aidan : MonoBehaviour {

    public RectTransform mainMenu;
    public RectTransform settingsMenu;

    bool isInMainMenuScene = false;

	public void PlayAgain()
    {
        Cursor.visible = false;
        isInMainMenuScene = false;
        SceneManager.LoadScene("GrayBox_Done");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        isInMainMenuScene = true;
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
}
