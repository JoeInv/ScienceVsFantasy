using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Towers : MonoBehaviour
{
    //Parent class for Towers
    public int health = 50;
    public int cost;

    public Vector3Int cellPos;
    public Tilemap tilemap;
    public float interval = 5f;
    public int actionVal;
    public float buyCooldown = 10f;
    public virtual bool LoseHealth(int val)
    {
        //If a tower gets hit they lose the health and play a sound
        health -= val;
        SoundManager.instance.TowerHit();
        if(health<= 0)
        {
            //When a tower is destroyed the tile is reset
            RestoreTile();
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public void RestoreTile()
    {
        //Resets tile to Sprite so towers can be placed again
        if (tilemap != null)
        {
            tilemap.SetColliderType(cellPos, Tile.ColliderType.Sprite);
        }

    }

}
