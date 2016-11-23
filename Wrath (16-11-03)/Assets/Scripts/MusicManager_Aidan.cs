using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager_Aidan : MonoBehaviour {

    public AudioClip menuTheme;
    public AudioClip ambientTheme;
    public float musicFadeTime;

    string sceneName;

    void Start()
    {
        OnLevelWasLoaded(0);
    }

    void OnLevelWasLoaded(int scene)
    {
        string newSceneName = SceneManager.GetActiveScene().name;

        if (newSceneName != sceneName)
        {
            sceneName = newSceneName;
            Invoke("PlayMusic", 0.2f);
        }
    }

    void PlayMusic()
    {
        AudioClip clipToPlay = null;

        if (sceneName == "MainMenuScene")
        {
            clipToPlay = menuTheme;
        }
        else if (sceneName == "peter_working_scene")
        {
            clipToPlay = ambientTheme;
        }

        if (clipToPlay != null)
        {
            AudioManager_Aidan.instance.PlayMusic(clipToPlay, musicFadeTime);
            Invoke("PlayMusic", clipToPlay.length);
        }
    }
}
