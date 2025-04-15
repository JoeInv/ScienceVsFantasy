using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    void Awake()
    {
        instance = this;
    }
    public List<GameObject> prefabs;
    public List<Transform> spawnPoints;
    public float spawnInterval = 10f;

    public void StartSpawning()
    {
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        while (true)
        {
            SpawnEnemy(); 
            yield return new WaitForSeconds(spawnInterval); //uses whatever the enemy spawn rate is
        }
    }

    void SpawnEnemy()
    {
        SoundManager.instance.EnemySpawned();
        //Spawns random enemy at one of the 6 random spawn points
        int randomPrefabID = Random.Range(0, prefabs.Count); 
        int randomSpawnPointID = Random.Range(0, spawnPoints.Count);
        GameObject spawnedEnemy = Instantiate(prefabs[randomPrefabID], spawnPoints[randomSpawnPointID]);
        Vector3 pos = spawnedEnemy.transform.position;
        pos.z = 0; //setting the z = 0  since they spawn at -10
        spawnedEnemy.transform.position = pos;


    }
}
