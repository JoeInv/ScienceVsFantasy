using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObj;
    public GameObject currContainer;
    public float playerEnergy = 100f;
    public TMP_Text energyText;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateEnergy();
    }

    public bool CanAfford(float cost)
    {
        return playerEnergy >= cost;
    }

    public void SpendEnergy(float cost)
    {
        playerEnergy -= cost;
        Debug.Log(playerEnergy);
    }

    public void UpdateEnergy()
    {
        energyText.text = "Energy:\n" + playerEnergy.ToString();
    }

    public void PlaceObject(float towerCost)
    {
        if (draggingObj != null && currContainer != null)
        {
            if (CanAfford(towerCost))
            {
                Instantiate(draggingObj.GetComponent<TowerDragging>().card.objectGame, currContainer.transform);
                currContainer.GetComponent<TowerContainers>().isFull = true;
                SpendEnergy(towerCost);
                UpdateEnergy();
            }
        }
    }
}
