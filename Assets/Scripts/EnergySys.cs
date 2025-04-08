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
        energy += val;
        UpdateUI();
    }

    public bool Use(int val)
    {
        if (EnoughEnergy(val))
        {
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
            return true;
        else
        {
            return false;
        }
    }
    void UpdateUI()
    {
        energyText.text = energy.ToString();
    }
}
