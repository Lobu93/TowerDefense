using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems; // Required when using Event data.

public class UnitButtons : MonoBehaviour, IClickable, IPointerDownHandler
{
    public GameObject defenderPrefab;
    public static GameObject selectedDefender;

    private UnitButtons[] buttonArray; 
    private TextMeshProUGUI costText;

    // Start is called before the first frame update
    void Start()
    {
        buttonArray = FindObjectsOfType<UnitButtons>();

        costText = GetComponentInChildren<TextMeshProUGUI>();
        if (!costText) { Debug.LogWarning(name + " has no cost text"); }

        costText.text = "$" + defenderPrefab.GetComponent<UnitData>().levels[0].cost.ToString();

        //addPhysics2DRaycaster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnMouseDown()
    //{
    //    foreach (Button thisButton in buttonArray)
    //    {
    //        thisButton.GetComponent<SpriteRenderer>().color = Color.black;
    //        thisButton.GetComponent<Image>().color = Color.black;
    //    }

    //    GetComponent<SpriteRenderer>().color = Color.white;
    //    GetComponent<Image>().color = Color.white;
    //    selectedDefender = defenderPrefab;

    //    print("private void OnMouseDown()");
    //}

    public void Click()
    {
        print("Butão cricado em Button.cs");
    }

    void addPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = GameObject.FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log(this.gameObject.name + " Was Clicked.");

        foreach (UnitButtons thisButton in buttonArray)
        {
            thisButton.GetComponent<Image>().color = Color.gray;
            thisButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;

            // Debug.Log(thisButton.name + " Was Clicked.");
        }

        GetComponent<Image>().color = Color.white;
        costText.color = new Color(1.0f, 0.8431373f, 0.0f);
        selectedDefender = defenderPrefab;
    }
}
