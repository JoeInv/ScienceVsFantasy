using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    public TMP_Text enemiesLeft; //For showing how many enemies are left
    int enemiesKilled; //To keep track of killed enemy count
    public List<GameObject> prefabs; //List of enemy prefabs
    public List<Transform> spawnPoints; //List of the 6 different spawn points
    private List<int> spawnQueue; //Controls what order enemies will spawn
    private List<float> enemyDelay; //Controls the delay between enemy spawns
    private int enemySpawned = 0;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        enemiesKilled = 0;
    }
    public void StartSpawning()
    { //Had ChatGPT help me figure out the order of the waves and the timing for them spawning in so I didn't have to sit there trying to figure out the perfect wave.
    spawnQueue = new List<int> {
    0, 0, 0, 0, 0, 0,
    0, 1, 0, 0, 1, 0,
    1, 0, 1, 0, 1, 1,
    1, 0, 1, 1, 0, 1,
    1, 0, 0, 1, 0, 1,
    2, 1, 0, 1, 2, 1,
    2, 0, 1, 2, 1, 2,
    1, 2, 0, 1, 2,
    0, 1, 2, 1, 0
    };

    enemyDelay = new List<float> {
    5f, 5f, 4.5f, 4.5f, 4f, 4f,
    3.8f, 4f, 3.6f, 3.6f, 3.5f, 3.5f,
    3.2f, 3.2f, 3.2f, 3f, 3f, 3f,
    2.8f, 2.8f, 2.5f, 2.5f, 2.5f, 2.5f,
    2.2f, 2.2f, 2.2f, 2f, 2f, 2f,
    1.8f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f,
    1.2f, 1.2f, 1.2f, 1.2f, 1f, 1f,
    0.9f, 1f, 0.9f, 1f, 0.8f,
    0.8f, 0.7f, 0.7f, 0.7f, 0.5f
    };
        enemySpawned = 0;
        StartCoroutine(SpawnDelay());
    

    IEnumerator SpawnDelay()
    {
        while (enemySpawned < spawnQueue.Count)
        {
        if (enemySpawned < enemyDelay.Count)
        {
            float delay = enemyDelay[enemySpawned];
            yield return new WaitForSeconds(delay); //uses whatever delay is up in the list
        }
        else
        {
            Debug.Log("No matching delay found");
            yield break;
        }
            SpawnEnemy();
            enemySpawned++; //Adds to count of spawned enemies so it can go to the next pair
        }
    }}

    void SpawnEnemy()
    {
        if (enemySpawned >= spawnQueue.Count)
        return;
        SoundManager.instance.EnemySpawned();
        //Following the order of the spawn queue enemies spawn in one of the 6 spawn points
        int enemyID = spawnQueue[enemySpawned];
        int randomSpawnPointID = Random.Range(0, spawnPoints.Count);
        GameObject spawnedEnemy = Instantiate(prefabs[enemyID], spawnPoints[randomSpawnPointID]);
        
        Vector3 pos = spawnedEnemy.transform.position;
        pos.z = 0; //setting the z = 0  since they spawn at -10
        spawnedEnemy.transform.position = pos;
        UpdateProgress(); //Updating to get the total. Probably not the best way for me to go about it tho
    }

    public void EnemyKilled()
    {
        enemiesKilled++;  //Adds to enemiesKilled count and then updates the display
        UpdateProgress();
    }

    void UpdateProgress()
    {
        enemiesLeft.text = $"{enemiesKilled} / {spawnQueue.Count}";
        if (enemiesKilled == spawnQueue.Count) //If all enemies have been spawned and killed the Win Screen will display
        {
            GameManager.instance.WinScreen();
        }
    }
}
