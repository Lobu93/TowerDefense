using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hub : MonoBehaviour
{
    public GameObject[] levelButtonArray;

    private int[] Levels = new int[5];

    private string selectedLevelName;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefsManager.IsLevelUnlocked(1))
        {
            PlayerPrefsManager.UnlockLevel(1);
        }

        UpdateMissionsUnlocked();
    }

    private void UpdateMissionsUnlocked()
    {
        for (int i = 0; i < levelButtonArray.Length; i++)
        {
            if (PlayerPrefsManager.IsLevelUnlocked(i + 1))
            {
                levelButtonArray[i].GetComponent<Button>().enabled = true;
                levelButtonArray[i].GetComponent<Image>().color = Color.white;
                levelButtonArray[i].GetComponentInChildren<TextMeshProUGUI>().color = new Color(1.0f, 0.8431373f, 0.0f);
            }
            else
            {
                levelButtonArray[i].GetComponent<Button>().enabled = false;
                levelButtonArray[i].GetComponent<Image>().color = Color.gray;
                levelButtonArray[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
            }
        }
    }

    public void SelectLevel(string levelName)
    {
        selectedLevelName = levelName;

        Debug.Log("selectedLevelName: " + selectedLevelName);
    }

    public void PlaySelectedLevel()
    {
        if (selectedLevelName != null)
        {
            LoadingData.sceneToLoad = selectedLevelName;
            SceneManager.LoadScene("01d LoadingScreen");
        }
    }
}
