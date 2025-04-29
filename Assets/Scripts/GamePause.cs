using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject warningMessage;
    
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        Time.timeScale = 0f; //Pauses game
    }
    
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        Time.timeScale = 1f; //Unpauses game
    }

    public void QuitToMenu()
    {
        warningMessage.SetActive(true); //Shows warning message of how leaving the game will cause the level to reset
        if (warningMessage.activeSelf) //If message is active and the button is pressed again then it will return to menu
        {
            Time.timeScale = 1f;
            warningMessage.SetActive(false);
            SceneManager.LoadScene("MainMenu");
        }
    }
    public void Retry()
    {
        //Resets game and reloads scene
        Time.timeScale = 1f;
        if (GameManager.instance != null)
            Destroy(GameManager.instance.gameObject);
        if (SoundManager.instance != null)
            Destroy(SoundManager.instance.gameObject);
        EnemySpawner.instance = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
