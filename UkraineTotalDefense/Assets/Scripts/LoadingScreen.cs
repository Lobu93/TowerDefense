using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingData
{
    public static string sceneToLoad;
}

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] Image _progressBar;
    [SerializeField] TextMeshProUGUI _progressText;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(WaitForFunction());

        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // The operation that will control the Async loading using the global LoadingData script
        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);

        // Stop next scene from loading
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Here you can do whatever you want, from displaying tips to anything else
            _progressBar.fillAmount = operation.progress;
            _progressText.text = operation.progress * 100 + "%";

            if (operation.progress >= 0.9f)
            {
                // Allow next scene to load
                operation.allowSceneActivation = true;
            }

            // yield return null;

            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Hello!");
        StartCoroutine(LoadSceneAsync());
    }
}
