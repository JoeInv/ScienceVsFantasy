using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    //Audio Sources
    public AudioSource sfxSource;
    public AudioSource musicSource;

    //Audio Clips
    public AudioClip plasmaTowerShoot;
    public AudioClip energyGenerated;
    public AudioClip towerSelect;
    public AudioClip towerPlace;
    public AudioClip buttonActivated;
    public AudioClip crazyLaser;
    public AudioClip towerHit;
    public AudioClip enemySpawned;
    public AudioClip bombBlast;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //The Mute Buttons for sound and music
    public void MuteSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        TowerSelect();
    }
    public void MuteMusic()
    {
        musicSource.mute = !musicSource.mute;
        TowerSelect();
    }
    //All of the sounds being played below
    public void PlasmaBlast()
    {
        sfxSource.PlayOneShot(plasmaTowerShoot, 0.15f);
    }

    public void EnergyGenerated()
    {
        sfxSource.PlayOneShot(energyGenerated, 0.15f);
    }
    public void CrazyLaser()
    {
        sfxSource.PlayOneShot(crazyLaser, 0.15f);
    }
    public void TowerSelect()
    {
        sfxSource.PlayOneShot(towerSelect, 0.5f);
    }

    public void TowerPlace()
    {
        sfxSource.PlayOneShot(towerPlace, 0.5f);
    }

    public void TowerHit()
    {
        sfxSource.PlayOneShot(towerHit, 0.15f);
    }

    public void EnemySpawned()
    {
        sfxSource.PlayOneShot(enemySpawned, 0.15f);
    }
    
    public void ButtonActivated()
    {
        sfxSource.PlayOneShot(buttonActivated, 0.4f);
    }

    public void BombBlast()
    {
        sfxSource.PlayOneShot(bombBlast, 0.8f);
    }
    
}
