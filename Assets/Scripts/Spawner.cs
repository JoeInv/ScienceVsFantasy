using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Image = UnityEngine.UI.Image;

public class Spawner : MonoBehaviour
{
    public List<GameObject> towerPrefabs; //List of the actual Towers
    public Transform spawnTower;
    public List<Image> towerUI; //List of Tower Images 
    int spawnID = -1; //resets spawnID
    public Tilemap spawnTilemap;
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
        float towerCooldown = towerPrefabs[towerID].GetComponent<Towers>().buyCooldown;
        towerUI[towerID].raycastTarget = false; //Disables the button
        Color iconColor = towerUI[towerID].color;
        float timer = 0f;
        while (timer < towerCooldown) //Tower slowly fades back in over the cooldown duration
        {
            float alpha = timer / towerCooldown;
            towerUI[towerID].color = new Color(iconColor.r, iconColor.g, iconColor.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }
        //Resets the color of the icon to normal to be safe
        iconColor.a = 1f;
        towerUI[towerID].color = iconColor;
        towerUI[towerID].raycastTarget = true; //Renables button
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
                    GameManager.instance.energy.Use(towerCost); //Subtracts the cost of the tower from the energy
                    int towerID = spawnID; //Need to store since SpawnTower deselects afterwards
                    SpawnTower(cellPosCentered);
                    spawnTilemap.SetColliderType(cellPosDefault,Tile.ColliderType.None); //Sets collider to none so nothing else is placed
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
        SoundManager.instance.TowerPlace();
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
        SoundManager.instance.TowerSelect();
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
