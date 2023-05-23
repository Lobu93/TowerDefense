using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider soundVXVolumeSlider;
    public LevelManager levelManager;

    private MusicManager musicManager;

    // Start is called before the first frame update
    void Start()
    {
        musicManager = GameObject.FindObjectOfType<MusicManager>();

        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();

        soundVXVolumeSlider.value = PlayerPrefsManager.GetSoundFXVolume();
    }

    // Update is called once per frame
    void Update()
    {
        musicManager.SetVolume(volumeSlider.value);
    }

    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        PlayerPrefsManager.SetSoundFXVolume(soundVXVolumeSlider.value);
        levelManager.LoadLevel("01a Start");
    }

    public void SetDefaults()
    {
        volumeSlider.value = 0.8f;
        soundVXVolumeSlider.value = 0.8f;
    }
}
