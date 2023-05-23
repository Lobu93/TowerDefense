using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required when using Event data.

public class StopButton : MonoBehaviour, IPointerDownHandler
{
    private LevelManager levelManager;
    private GameManagerBehavior gameManager;
    
    [SerializeField]
    private GameObject exitPanel;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    //private void OnMouseDown()
    //{
    //    levelManager.LoadLevel("01a Start");

    //    Debug.Log("class StopButton::OnMouseDown()");
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        gameManager.PauseGame();
        exitPanel.SetActive(true);
        Debug.Log("class StopButton::OnPointerDown(PointerEventData eventData)");
    }

    public void ExitToMenu()
    {
        gameManager.ResumeGame();
        levelManager.LoadLevel("01a Start");
    }

    public void BackToGame()
    {
        exitPanel.SetActive(false);
        gameManager.ResumeGame();
    }
}
