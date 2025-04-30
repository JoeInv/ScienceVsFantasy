using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelect;
    public GameObject settingsMenu;
    public GameObject infoMenu;
    public SoundManager soundManager;

    public void LevelSelect()
    {
        //Opens Level Select and closes other ones
        soundManager.TowerSelect();
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        settingsMenu.SetActive(false);
        infoMenu.SetActive(false);
    }
    public void SettingsMenu()
    {
        //Opens Settings menu and closes other ones
        soundManager.TowerSelect();
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        settingsMenu.SetActive(true);
        infoMenu.SetActive(false);
    }
    public void InfoMenu()
    {
        //Opens Info menu and closes other ones
        soundManager.TowerSelect();
        mainMenu.SetActive(false);
        levelSelect.SetActive(false);
        settingsMenu.SetActive(false);
        infoMenu.SetActive(true);
    }
    public void BackToMainMenu()
    {
        //Goes back to main menu
        soundManager.TowerSelect();
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        settingsMenu.SetActive(false);
        infoMenu.SetActive(false);
    }
    public void Quit()
    {
        Debug.Log("Closing Game");
        soundManager.TowerSelect();
        Application.Quit();
    }
    public void LoadLevel(string sceneName)
    {
        soundManager.TowerSelect();
        SceneManager.LoadScene(sceneName);
    }
}
