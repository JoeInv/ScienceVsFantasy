using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : Towers
{
    public GameObject explosion;
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
            //When the bomb tower dies it explodes and kills nearby enemies around it
            GoBoom(); 
            RestoreTile();
            return true;
        }
        return false;
    }

    public void GoBoom()
    {
        GameObject blast = Instantiate(explosion, transform.position, Quaternion.identity); //Decided to do particles for an explosion rather than animating it
        //Deals damage to enemies in a 1 tile radius with a little extra to account for error
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.1f); //Attacks enemies in a 1 unit radius with a little extra to account for error
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
        SoundManager.instance.BombBlast();
        Destroy(gameObject);
    }

}
