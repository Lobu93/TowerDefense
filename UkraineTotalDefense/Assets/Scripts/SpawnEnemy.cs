using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2;
    public int maxEnemies = 20;
}

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject testEnemyPrefab;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private GameManagerBehavior gameManager;
    private GameObject parent;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();

        parent = GameObject.Find("Defenders");

        if (!parent)
        {
            parent = new GameObject("Defenders");
        }
    }

    // Update is called once per frame
    void Update()
    {
        int currentWave = gameManager.Wave;
        if (currentWave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) &&
                enemiesSpawned < waves[currentWave].maxEnemies)
            {
                lastSpawnTime = Time.time;
                GameObject newEnemy = (GameObject) Instantiate(waves[currentWave].enemyPrefab);
                newEnemy.transform.parent = parent.transform; // Attach to Defenders empty object
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                enemiesSpawned++;
            }
            
            if (enemiesSpawned == waves[currentWave].maxEnemies &&
                GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                gameManager.Wave++;
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
        }
        else
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }

    }
}
