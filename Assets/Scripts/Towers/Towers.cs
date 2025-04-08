using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{
    public int health = 50;
    public int cost;
    public float interval = 5f;
    public int actionVal;
    public float buyCooldown = 10f;
    public virtual bool LoseHealth(int val)
    {
        health -= val;
        if(health<= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

}
