using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using TMPro.EditorUtilities;

public class EnergySys : MonoBehaviour
{
    public TMP_Text energyText;
    public int defaultEnergy = 100;
    public int energy;

    public void Start()
    {
        energy = defaultEnergy;
        UpdateUI();
    }

    public void Gain(int val)
    {
        //Adds energy and updates UI
        energy += val;
        UpdateUI();
    }

    public bool Use(int val)
    {
        if (EnoughEnergy(val))
        {
            //Subtracts cost from energy
            energy -= val;
            UpdateUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool EnoughEnergy(int val)
    {
        if(val <= energy)
        //Checks whether the player has enough for the tower
            return true;
        else
        {
            return false;
        }
    }
    void UpdateUI()
    {
        //Updates the energy display
        energyText.text = energy.ToString();
    }
}
