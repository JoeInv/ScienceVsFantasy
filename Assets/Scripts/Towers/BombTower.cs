using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : Towers
{
    private void Start()
    {
        cost = 25;
    }

    public override bool LoseHealth(int val)
    {
        health -= val;
        SoundManager.instance.TowerHit();
        if (health <= 0)
        {
            GoBoom();
            RestoreTile();
            return true;
        }
        return false;
    }

    public void GoBoom()
    {
        //Deals damage to enemies in a 1 tile radius with a little extra to account for error
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.LoseHealth(actionVal);
                }
            }
        }
        //SoundManager.instance.BombBlast();
        Destroy(gameObject);
    }

}
