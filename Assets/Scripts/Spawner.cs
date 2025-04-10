using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Image = UnityEngine.UI.Image;

public class Spawner : MonoBehaviour
{
    public SpriteRenderer testSprite;
    public List<GameObject> towerPrefabs;
    public Transform spawnTower;
    public List<Image> towerUI;

    int spawnID = -1;
    public Tilemap spawnTilemap;
    float towerCooldown;
    Towers towers;
    void Update()
    {
        if(CanSpawn())
            DetectSpawnPoint();
    }
    bool CanSpawn()
    {
        if (spawnID == -1) //if the player clicks on a tower the id won't be -1
        return false;
        else
        return true;
    }

    IEnumerator Cooldown(int towerID)
    {
        //Disables the tower UI in game so the player can't buy till the cooldown is over
        //May add cooldown timer in the future so the player has a better idea
        towerCooldown = towerPrefabs[towerID].GetComponent<Towers>().buyCooldown;
        towerUI[towerID].enabled = false;
        yield return new WaitForSeconds(towerCooldown);
        towerUI[towerID].enabled = true;
    }

    void DetectSpawnPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            var cellPosDefault = spawnTilemap.WorldToCell(mousePos);
            var cellPosCentered = spawnTilemap.GetCellCenterWorld(cellPosDefault);
            
            if(spawnTilemap.GetColliderType(cellPosDefault)==Tile.ColliderType.Sprite)
            {
                int towerCost = TowerCost(spawnID);
                if(GameManager.instance.energy.EnoughEnergy(towerCost))
                {
                    GameManager.instance.energy.Use(towerCost);
                    int towerID = spawnID; //Need to store since SpawnTower deselects afterwards
                    SpawnTower(cellPosCentered);
                    spawnTilemap.SetColliderType(cellPosDefault,Tile.ColliderType.None);
                    StartCoroutine(Cooldown(towerID)); 
                }
                else
                {
                    StartCoroutine(Error()); 
                }

        }
        
    }
    }

    public int TowerCost(int id)
    {
        switch(id)
        {
            case 0: return towerPrefabs[id].GetComponent<GenTower>().cost;
            case 1: return towerPrefabs[id].GetComponent<PlasmaTower>().cost;
            case 2: return towerPrefabs[id].GetComponent<WallerTower>().cost;
            case 3: return towerPrefabs[id].GetComponent<BombTower>().cost;
            default: return -1;
        }
    }

    void SpawnTower(Vector3 position)
    {
        GameObject tower = Instantiate(towerPrefabs[spawnID], spawnTower);
        tower.transform.position = position;
        Vector3Int cellPos = spawnTilemap.WorldToCell(position);
        towers = tower.GetComponent<Towers>();
        towers.cellPos = cellPos;
        towers.tilemap = spawnTilemap;
        DeselectTower(); 
    }

    IEnumerator Error()
    {
        //Flashes UI Tower Red so that the player can tell they don't have enough
        towerUI[spawnID].color = Color.red;
        yield return new WaitForSeconds(1f); 
        foreach(var t in towerUI)
        {
            t.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }
    public void SelectTower(int id)
    {
        DeselectTower();
        spawnID = id;
        towerUI[spawnID].color = Color.white;
    }

    public void DeselectTower()
    {
        spawnID = -1; //sets to null
        foreach(var t in towerUI)
        {
            t.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }
}
