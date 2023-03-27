using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceUnit : MonoBehaviour
{
    public GameObject unitPrefab;
    public GameObject selectedDecal;

    private PlaceUnit[] placeUnits;
    private GameObject unit;
    private GameManagerBehavior gameManager;

    private GameObject upgradePanel;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

        placeUnits = FindObjectsOfType<PlaceUnit>();

        upgradePanel = gameManager.upgradePanel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool CanPlaceUnit()
    {
        int cost = unitPrefab.GetComponent<UnitData>().levels[0].cost;
        return unit == null && gameManager.Gold >= cost;
    }

    void OnMouseUp()
    {
        unitPrefab = Button.selectedDefender;

        if (unitPrefab != null)
        {
            if (CanPlaceUnit())
            {
                unit = (GameObject)Instantiate(unitPrefab, transform.position, Quaternion.identity);
           
                UnitLevel currentUnit = unit.GetComponent<UnitData>().CurrentLevel;
                selectedDecal = currentUnit.selectedDecal;
                selectedDecal.SetActive(false);

                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.PlayOneShot(audioSource.clip);

                gameManager.Gold -= unit.GetComponent<UnitData>().CurrentLevel.cost;
                gameManager.currentOpenspot = gameObject;
            }
            else if (CanUpgradeUnit())
            {
                DeselectAllUnits();
                selectedDecal.SetActive(true);
                
                // ClearAllButtonDelegates();
                UpdateUpgradeButtonImage();
                gameManager.currentOpenspot = gameObject;

                gameManager.UpgradeCost = unit.GetComponent<UnitData>().CurrentLevel.cost;
                gameManager.unitNameLabel.text = unit.GetComponent<UnitData>().CurrentLevel.unitName.ToString();
                gameManager.upgradePanel.SetActive(true);

                // Debug.Log("Class: PlaceUnit | else if (CanUpgradeUnit())");
            }
            else
            {
                if (unit != null)
                {
                    DeselectAllUnits();
                    selectedDecal.SetActive(true);
                    UpdateUpgradeButtonImage();
                    // gameManager.currentOpenspot = gameObject;
                    gameManager.upgradeCostLabel.text = "Upgrade: MAX";
                    gameManager.unitNameLabel.text = unit.GetComponent<UnitData>().CurrentLevel.unitName.ToString();
                    gameManager.upgradePanel.SetActive(true);
                }
            }
        }
    }

    private bool CanUpgradeUnit()
    {
        if (unit != null)
        {
            UnitData monsterData = unit.GetComponent<UnitData>();
            UnitLevel nextLevel = monsterData.GetNextLevel();
            if (nextLevel != null)
            {
                return gameManager.Gold >= nextLevel.cost;
            }
        }
        return false;
    }

    private void DeactivateOpenspotSprites()
    {
        foreach (PlaceUnit placeUnit in placeUnits)
        {
            SpriteRenderer spriteRenderer = placeUnit.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.clear;
        }
    }

    public void DeselectAllUnits()
    {
        UnitData[] allUnits = FindObjectsOfType<UnitData>();        

        foreach (UnitData unitData in allUnits)
        {
            unitData.CurrentLevel.selectedDecal.SetActive(false);
        }                
    }

    public void UpgradeUnit()
    {
        if (CanUpgradeUnit())
        {
            // print("Upgrade Button pressed! " + gameObject);

            unit.GetComponent<UnitData>().IncreaseLevel();
            UpdateUpgradeButtonImage();

            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            gameManager.Gold -= unit.GetComponent<UnitData>().CurrentLevel.cost;
            gameManager.UpgradeCost = unit.GetComponent<UnitData>().CurrentLevel.cost;
            gameManager.unitNameLabel.text = unit.GetComponent<UnitData>().CurrentLevel.unitName.ToString();

            print("currentLevelIndex = " + unit.GetComponent<UnitData>().CurrentLevelIndex);
            Debug.Log("Class: PlaceUnit | Function: UpgradeUnit() | ObjectName: " + gameObject);
        }
    }

    private void UpdateUpgradeButtonImage()
    {
        Image upgradeImage = upgradePanel.GetComponentInChildren<Image>();
        UnitLevel currentLevel = unit.GetComponent<UnitData>().CurrentLevel;
        int currentLevelIndex = unit.GetComponent<UnitData>().levels.IndexOf(currentLevel);
        upgradeImage.sprite = upgradePanel.GetComponent<UpgradeButton>().buttonLevelImages[currentLevelIndex];
    }
}
