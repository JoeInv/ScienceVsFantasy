using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loseScreen;
    public GameObject winScreen;
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

    void OnEnable()
    {
        Enemy.OnGameOver += LoseScreen; //Subscribes to the gameover event
    }

    void OnDisable()
    {
        Enemy.OnGameOver -= LoseScreen; 
    }

    public void LoseScreen()
    {
        //Pauses game and shows lose screen
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void WinScreen()
    {
        //Pauses game and shows win screen
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    void Start()
    {
        StartCoroutine(WaveStartDelay());
    }
    IEnumerator WaveStartDelay()
    {
        //Gives player prep time before the enemies start to spawn
        waveText.enabled = true;
        waveText.text = "Enemies approaching soon...";
        yield return new WaitForSeconds(12f);
        waveText.text = "Here they come";
        yield return new WaitForSeconds(3f);
        waveText.enabled = false;
        EnemySpawner.instance.StartSpawning();
    }

}
