using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaTower : Towers
{
    public GameObject blastPrefab;
    void Start()
    {
        cost = 100;
        actionVal = 25;
        interval = 3f;
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            ShootItem();
        }
    }

    void ShootItem()
    {
        GameObject itemShot = Instantiate(blastPrefab, transform);
        itemShot.GetComponent<PlasmaBlast>().Init(actionVal);
    }

}
