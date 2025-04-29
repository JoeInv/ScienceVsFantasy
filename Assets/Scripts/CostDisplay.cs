using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CostDisplay : MonoBehaviour
{
    public int towerID;
    public int towerCost;
    public TMP_Text towerCostText;
    void Start()
    {
        towerCost = GameManager.instance.spawner.TowerCost(towerID); //Gets the tower cost from gamemanager spawner based on whatever the tower ID is
        towerCostText.text = towerCost.ToString(); //Displays it
    }
}
