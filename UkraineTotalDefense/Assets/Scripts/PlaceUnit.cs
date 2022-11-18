using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceUnit : MonoBehaviour
{
    public GameObject unitPrefab;
    public GameObject selectedDecal;    

    private PlaceUnit[] placeUnits;
    private GameObject unit;
    private GameManagerBehavior gameManager;

    private GameObject upgradeButton;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

        placeUnits = FindObjectsOfType<PlaceUnit>();

        upgradeButton = gameManager.upgradePanel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool CanPlaceMonster()
    {
        int cost = unitPrefab.GetComponent<UnitData>().levels[0].cost;
        return unit == null && gameManager.Gold >= cost;
    }

    void OnMouseUp()
    {
        unitPrefab = Button.selectedDefender;

        if (unitPrefab != null)
        {
            if (CanPlaceMonster())
            {
                unit = (GameObject)Instantiate(unitPrefab, transform.position, Quaternion.identity);

                //UpgradeButton upgradeButtonDelegate = upgradeButton.GetComponent<UpgradeButton>();

                //if (upgradeButtonDelegate)
                //{
                //    upgradeButtonDelegate.upgradeDelegate += UpgradeUnit;

                //    if (upgradeButtonDelegate.upgradeDelegate != null)
                //    {
                //        print("UpgradeButtonDelegate in ON! " + gameObject);
                //    }

                //    // print("UpgradeButton.Unit set successful!");
                //}
                //else
                //{
                //    Debug.LogError("UpgradeButton.Unit failed...");
                //}

                UnitLevel currentUnit = unit.GetComponent<UnitData>().CurrentLevel;
                selectedDecal = currentUnit.selectedDecal;
                selectedDecal.SetActive(false);

                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                audioSource.PlayOneShot(audioSource.clip);

                gameManager.Gold -= unit.GetComponent<UnitData>().CurrentLevel.cost;
            }
            else if (CanUpgradeUnit())
            {
                DeselectAllUnits();
                selectedDecal.SetActive(true);

                ClearAllButtonDelegates();

                //UpgradeButton upgradeButtonDelegate = upgradeButton.GetComponent<UpgradeButton>();

                //if (upgradeButtonDelegate)
                //{
                //    ClearAllButtonDelegates();

                //    upgradeButtonDelegate.upgradeDelegate += UpgradeUnit;

                //    if (upgradeButtonDelegate.upgradeDelegate != null)
                //    {
                //        print("UpgradeButtonDelegate in ON! " + gameObject);
                //    }
                //}
                //else
                //{
                //    Debug.LogError("UpgradeButton.Unit failed...");
                //}

                gameManager.upgradeCostLabel.text = unit.GetComponent<UnitData>().CurrentLevel.cost.ToString();
                gameManager.upgradePanel.SetActive(true);
            }
            else
            {
                DeselectAllUnits();
                selectedDecal.SetActive(true);
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
        // PlaceUnit[] allPlaceUnits = FindObjectsOfType<PlaceUnit>();

        foreach (UnitData unitData in allUnits)
        {
            unitData.CurrentLevel.selectedDecal.SetActive(false);
        }

        /* foreach (PlaceUnit placeUnit in allPlaceUnits)
        {
            UpgradeButton upgradeButtonDelegate = placeUnit.upgradeButton.GetComponent<UpgradeButton>();
            
            upgradeButtonDelegate.upgradeDelegate -= UpgradeUnit;

            if (upgradeButtonDelegate)
            {
                Debug.LogWarning("DeselectedUnit: " + placeUnit.name);
            }
        } */
    }

    private void ClearAllButtonDelegates()
    {
        PlaceUnit[] allPlaceUnits = FindObjectsOfType<PlaceUnit>();
        UpgradeButton upgradeButtonDelegate;

        foreach (PlaceUnit placeUnit in allPlaceUnits)
        {
            if (gameObject != placeUnit.gameObject)
            {
                upgradeButtonDelegate = placeUnit.upgradeButton.GetComponent<UpgradeButton>();

                upgradeButtonDelegate.upgradeDelegate -= UpgradeUnit;
            }
            else
            {
                //UpgradeButton upgradeButtonDelegate = upgradeButton.GetComponent<UpgradeButton>();
                //upgradeButtonDelegate.upgradeDelegate += UpgradeUnit;
                
                Debug.LogWarning("Only Unit selected: " + placeUnit.name);
            }
        }

        upgradeButtonDelegate = upgradeButton.GetComponent<UpgradeButton>();
        upgradeButtonDelegate.upgradeDelegate += UpgradeUnit;
    }

    public void UpgradeUnit()
    {
        if (CanUpgradeUnit())
        {
            print("Upgrade Button pressed! " + gameObject);

            unit.GetComponent<UnitData>().IncreaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            gameManager.Gold -= unit.GetComponent<UnitData>().CurrentLevel.cost;
        }        
    }
}
