using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStartVolume : MonoBehaviour
{
    private MusicManager musicManager;
    private float defaultVolume = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        musicManager = GameObject.FindObjectOfType<MusicManager>();

        if (musicManager)
        {
            if (PlayerPrefsManager.IsFirstGameInit())
            {
                musicManager.SetVolume(defaultVolume);
                PlayerPrefsManager.SetMasterVolume(defaultVolume);
            }
            else
            {
                float volume = PlayerPrefsManager.GetMasterVolume();
                musicManager.SetVolume(volume);
            }            
        }
        else
        {
            Debug.LogWarning("No music manager found in Start scene, can't set volume");
        }        
    }
}
