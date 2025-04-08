using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallerTower : Towers
{
    void Start()
    {
     health = 250;
     buyCooldown = 25f;
     cost = 50;
    }
}
