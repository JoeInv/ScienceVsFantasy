using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenTower : Towers
{
    public void Start()
    {
        actionVal = 25;
        cost = 50;
        StartCoroutine(Interval());
    }

    IEnumerator Interval()
    {
        //Generates energy based on the interval
        while (true)
        {
        yield return new WaitForSeconds(interval);
        AddEnergy();
        SoundManager.instance.EnergyGenerated();
        }
    }

    public void AddEnergy()
    {
        GameManager.instance.energy.Gain(actionVal);
    }

}
