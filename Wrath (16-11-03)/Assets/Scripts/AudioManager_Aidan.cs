using UnityEngine;
using System.Collections;

public class AudioManager_Aidan : MonoBehaviour {

    public enum AudioChannel { Master, SFX, Music};
    public float masterVolumePercent = 1f;
    public float sfxVolumePercent = 1f;
    public float musicVolumePercent = 1f;

    public static AudioManager_Aidan instance = null;

    int activeMusicSourceIndex;
    AudioSource[] musicSources;

    void Start()
    {
        instance = this;
        musicSources = new AudioSource[2];
        for (int i = 0; i < 2; i++)
        {
            GameObject newMusicSource = new GameObject("Music Source " + (i + 1));
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }

        masterVolumePercent = PlayerPrefs.GetFloat("masterVolume", masterVolumePercent);
        sfxVolumePercent = PlayerPrefs.GetFloat("sfxVolume", sfxVolumePercent);
        musicVolumePercent = PlayerPrefs.GetFloat("musicVolume", musicVolumePercent);
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        AudioManager_Aidan.instance.musicSources[activeMusicSourceIndex].clip = clip;
        AudioManager_Aidan.instance.musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.SFX:
                sfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
        }

        if (musicSources != null)
        {
            musicSources[0].volume = musicVolumePercent * masterVolumePercent;
            musicSources[1].volume = musicVolumePercent * masterVolumePercent;

            PlayerPrefs.SetFloat("masterVolume", masterVolumePercent);
            PlayerPrefs.SetFloat("sfxVolume", sfxVolumePercent);
            PlayerPrefs.SetFloat("musicVolume", musicVolumePercent);
            PlayerPrefs.Save();
        }
    }

    IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
            yield return null;
        }
    }
}
