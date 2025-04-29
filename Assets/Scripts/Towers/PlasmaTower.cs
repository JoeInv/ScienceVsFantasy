using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaTower : Towers
{
    public GameObject blastPrefab;
    public float detectRange = 20f;
    public LayerMask enemyMask;
    private Coroutine shoot;
    void Start()
    {
        cost = 100;
        actionVal = 25;
        interval = 3f;
        shoot = StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (InRange())
            //If enemy is in lane then the tower can shoot
                ShootItem();
            
        }
    }

    bool InRange()
    {
        //If there is an enemy in the lane then the plasma tower will shoot at it
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, detectRange, enemyMask);;
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }

    void ShootItem()
    {
        //Spawns the laser in front of the Plasma tower
        GameObject itemShot = Instantiate(blastPrefab, transform);
        SoundManager.instance.PlasmaBlast();
        itemShot.GetComponent<PlasmaBlast>().Init(actionVal);
    }

}
