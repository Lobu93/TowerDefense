using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required when using Event data.

public class StopButton : MonoBehaviour, IPointerDownHandler
{
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    //private void OnMouseDown()
    //{
    //    levelManager.LoadLevel("01a Start");

    //    Debug.Log("class StopButton::OnMouseDown()");
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        levelManager.LoadLevel("01a Start");
        Debug.Log("class StopButton::OnPointerDown(PointerEventData eventData)");
    }

}
