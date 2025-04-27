using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    public List<GameObject> prefabs; //List of enemy prefabs
    public List<Transform> spawnPoints; //List of the 6 different spawn points
    public float spawnInterval = 10f; 
    private List<int> spawnQueue; //Controls what order enemies will spawn
    private List<float> enemyDelay; //Controls the delay between enemy spawns
    private int enemySpawned = 0;
    MainMenu mainMenu;
    private string currentLevel;
    void Awake()
    {
        instance = this;
        mainMenu = FindObjectOfType<MainMenu>();
    }
    public void StartSpawning()
    {
            spawnQueue = new List<int> {0,0,0,0,};
            enemyDelay = new List<float> {0f, 3f, 1.5f, 0f,}; 
        enemySpawned = 0;
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        while (enemySpawned < spawnQueue.Count)
        {
            
            float delay = enemyDelay[enemySpawned];
            yield return new WaitForSeconds(delay); //uses whatever the enemy spawn rate is
            SpawnEnemy();
            enemySpawned++;
        }
    }

    void SpawnEnemy()
    {
        if (enemySpawned >= spawnQueue.Count)
        return;
        SoundManager.instance.EnemySpawned();
        //Following the order of the spawn queue enemies spawn in one of the 6 spawn points\
        int enemyID = spawnQueue[enemySpawned];
        int randomSpawnPointID = Random.Range(0, spawnPoints.Count);
        GameObject spawnedEnemy = Instantiate(prefabs[enemyID], spawnPoints[randomSpawnPointID]);
        
        Vector3 pos = spawnedEnemy.transform.position;
        pos.z = 0; //setting the z = 0  since they spawn at -10
        spawnedEnemy.transform.position = pos;

        UpdateProgress();
    }

    void UpdateProgress()
    {
        //Debug.Log("Enemies Remaining: " + enemiesLeft);
    }
}
