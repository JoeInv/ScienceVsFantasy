using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerContainers : MonoBehaviour
{
    public bool isFull;
    public GameManager gameManager;
    public Image backgroundImage;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.draggingObj != null && isFull == false)
        {
            gameManager.currContainer = this.gameObject;
            backgroundImage.enabled = true;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        gameManager.currContainer = null;
        backgroundImage.enabled = false;
    }

}
