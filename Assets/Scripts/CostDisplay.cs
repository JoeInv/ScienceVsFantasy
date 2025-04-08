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
        towerCost = GameManager.instance.spawner.TowerCost(towerID);
        towerCostText.text = towerCost.ToString();
    }
}
