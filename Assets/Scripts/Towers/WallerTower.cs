using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallerTower : Towers
{//Not much needed here since its just a tower that stands there and blocks enemies
    void Start()
    {
     health = 250;
     buyCooldown = 25f;
     cost = 50;
    }
}
