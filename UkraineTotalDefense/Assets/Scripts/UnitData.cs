using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitLevel
{
    public int cost;
    public GameObject visualization;
    public GameObject bullet;
    public float fireRate;
    public GameObject selectedDecal;
    public string unitName;
}

public class UnitData : MonoBehaviour
{
    public List<UnitLevel> levels;

    private UnitLevel currentLevel;

    public UnitLevel CurrentLevel
    {
        get
        {
            return currentLevel;
        }

        set
        {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel);

            GameObject levelVisualization = levels[currentLevelIndex].visualization;
            for (int i = 0; i < levels.Count; i++)
            {
                if (levelVisualization != null)
                {
                    if (i == currentLevelIndex)
                    {
                        levels[i].visualization.SetActive(true);
                    }
                    else
                    {
                        levels[i].visualization.SetActive(false);
                    }
                }
            }
        }
    }

    private int currentLevelIndex;
    public int CurrentLevelIndex
    {
        get
        {
            currentLevelIndex = levels.IndexOf(currentLevel);
            return currentLevelIndex;
        }
        set
        {
            currentLevelIndex = value;
            // upgradeCostLabel.GetComponent<TextMeshProUGUI>().text = "Upgrade: $" + upgradeCost;
        }
    }

    void OnEnable()
    {
        CurrentLevel = levels[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UnitLevel GetNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void IncreaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
        {
            CurrentLevel = levels[currentLevelIndex + 1];
        }
    }
}
