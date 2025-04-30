using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LaserBlast : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //If the laser hits an enemy it instantly destroys it
        if (collision.CompareTag("Enemy"))
        {
            EnemySpawner.instance.EnemyKilled();
            Destroy(collision.gameObject);
        }
    }
}
