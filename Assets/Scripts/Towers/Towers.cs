using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Towers : MonoBehaviour
{
    public int health = 50;
    public int cost;

    public Vector3Int cellPos;
    public Tilemap tilemap;
    public float interval = 5f;
    public int actionVal;
    public float buyCooldown = 10f;
    public virtual bool LoseHealth(int val)
    {
        health -= val;
        SoundManager.instance.TowerHit();
        if(health<= 0)
        {
            RestoreTile();
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    void RestoreTile()
    {
        if (tilemap != null)
        {
            tilemap.SetColliderType(cellPos, Tile.ColliderType.Sprite);
        }

    }

}
