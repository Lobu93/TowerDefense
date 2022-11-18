using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;

	void Start () 
	{
        if (autoLoadNextLevelAfter <= 0)
        {
			Debug.Log("Level auto load disabled, use a positive number in seconds");
        }
        else
        {
			Invoke("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}

    public void LoadLevel(string name)
    {
		Debug.Log ("New Level load: " + name);
		// Application.LoadLevel (name);
		SceneManager.LoadScene(name);
	}

    public void QuitRequest()
    {
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

    public void LoadNextLevel()
    {
		// Application.LoadLevel(Application.loadedLevel + 1);
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex + 1);
	}
}
