using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
        InitVolume();
    }

    private void InitVolume()
    {
        _musicSource.volume = PlayerPrefs.GetFloat("music", 1f);
        _sfxSource.volume = PlayerPrefs.GetFloat("sound", 1f);
    }

    public void SetMusicVolume(float value)
    {
        float volume = value / 100f;
        PlayerPrefs.SetFloat("music", volume);
        _musicSource.volume = volume;
    }

    public void SetSfxVolume(float value)
    {
        float volume = value / 100f;
        PlayerPrefs.SetFloat("sound", volume);
        _sfxSource.volume = volume;
    }

    public void PlaySFX(AudioClip sfx)
    {
        if (!_sfxSource.isPlaying)
        {
            _sfxSource.PlayOneShot(sfx);
        }
    }

    public void StopSFX()
    {
        _sfxSource.Stop();
    }

}
