using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master_volume";
    const string SOUNDFX_KEY = "sound_fx_volume";
    const string LEVEL_KEY = "level_unlocked_";
    const string FIRST_INIT_KEY = "first_init";

    public static void SetMasterVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume out of range");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static void UnlockLevel(int level)
    {
        if (level <= SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + level.ToString(), 1); // Use 1 for true            
        }
        else
        {
            Debug.LogError("Trying to unlock level not in build order");
        }
    }

    public static bool IsLevelUnlocked(int level)
    {
        int levelValue = PlayerPrefs.GetInt(LEVEL_KEY + level.ToString());
        bool isLevelUnlocked = (levelValue == 1);

        if (level <= SceneManager.sceneCountInBuildSettings - 1)
        {
            return isLevelUnlocked;      
        }
        else
        {
            Debug.LogError("Trying to query level not in build order");
            return false;
        }
    }

    public static void SetSoundFXVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(SOUNDFX_KEY, volume);
        }
        else
        {
            Debug.LogError("SoundFX volume out of range");
        }
    }

    public static float GetSoundFXVolume()
    {
        return PlayerPrefs.GetFloat(SOUNDFX_KEY);
    }

    public static bool IsFirstGameInit()
    {
        int initValue = PlayerPrefs.GetInt(FIRST_INIT_KEY);
        bool isFirstInit = (initValue == 1);

        if (isFirstInit)
        {
            return false;
        }
        else
        {
            PlayerPrefs.SetInt(FIRST_INIT_KEY, 1);
            return true;
        }
    }
}
