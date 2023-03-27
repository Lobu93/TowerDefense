using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    public Sprite[] buttonLevelImages;
    private GameManagerBehavior gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    public void OnUpgradeUnit()
    {
        gameManager.currentOpenspot.GetComponent<PlaceUnit>().UpgradeUnit();
    }
}
