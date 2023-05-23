using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerBehavior : MonoBehaviour
{
    public TextMeshProUGUI goldLabel;
    public TextMeshProUGUI waveLabel;
    public GameObject[] nextWaveLabels;
    public bool gameOver = false;
    public bool isGamePaused = false;
    public TextMeshProUGUI healthLabel;
    public TextMeshProUGUI upgradeCostLabel;
    public TextMeshProUGUI unitNameLabel;
    public GameObject upgradePanel;
    public GameObject currentOpenspot;

    private int gold;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldLabel.text = "MONEY: " + gold;
        }
    }

    private int wave;
    public int Wave
    {
        get
        {
            return wave;
        }
        set
        {
            wave = value;
            if (!gameOver)
            {
                for (int i = 0; i < nextWaveLabels.Length; i++)
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                }
            }
            waveLabel.text = "WAVE: " + (wave + 1) + "/" + GameObject.Find("Road").GetComponent<SpawnEnemy>().waves.Length;
        }
    }

    private int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (value < health)
            {
                Camera.main.GetComponent<CameraShake>().Shake();
            }
            
            health = value;
            healthLabel.text = "HEALTH: " + health;
            
            if (health <= 0 && !gameOver)
            {
                gameOver = true;
                GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
                gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
            }
        }
    }

    private int upgradeCost;
    public int UpgradeCost
    {
        get
        {
            return upgradeCost;
        }
        set
        {
            upgradeCost = value;
            upgradeCostLabel.GetComponent<TextMeshProUGUI>().text = "Upgrade: $" + upgradeCost;
        }
    }

    private int unitName;
    public int UnitName
    {
        get
        {
            return unitName;
        }
        set
        {
            unitName = value;
            unitNameLabel.text = unitName.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Gold = 1000;
        Wave = 0;
        Health = 5;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        isGamePaused = false;
    }
}
