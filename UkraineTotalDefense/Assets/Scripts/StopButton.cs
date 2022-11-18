using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButton : MonoBehaviour
{
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    private void OnMouseDown()
    {
        levelManager.LoadLevel("01a Start");
    }
}
