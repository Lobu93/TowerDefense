using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public GameObject defenderPrefab;
    public static GameObject selectedDefender;

    private Button[] buttonArray;
    private Text costText;

    // Start is called before the first frame update
    void Start()
    {
        buttonArray = FindObjectsOfType<Button>();

        costText = GetComponentInChildren<Text>();
        if (!costText) { Debug.LogWarning(name + " has no cost text"); }

        // costText.text = defenderPrefab.GetComponent<Defender>().starCost.ToString();
        costText.text = defenderPrefab.GetComponent<UnitData>().levels[0].cost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        foreach (Button thisButton in buttonArray)
        {
            thisButton.GetComponent<SpriteRenderer>().color = Color.black;
        }

        GetComponent<SpriteRenderer>().color = Color.white;
        selectedDefender = defenderPrefab;
    }
}
