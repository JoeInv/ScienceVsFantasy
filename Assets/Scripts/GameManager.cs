using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Spawner spawner;
    public int towerID;
    public int towerCost;
    public TMP_Text towerCostText;
    public TMP_Text waveText;
    public EnergySys energy;

    void Awake()
    {

        if(instance == null)
        {   
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        StartCoroutine(WaveStartDelay());
    }
    IEnumerator WaveStartDelay()
    {
        waveText.enabled = true;
        waveText.text = "Enemies approaching soon...";
        yield return new WaitForSeconds(12f);
        waveText.text = "Here they come";
        yield return new WaitForSeconds(3f);
        waveText.enabled = false;
        EnemySpawner.instance.StartSpawning();
    }

}
