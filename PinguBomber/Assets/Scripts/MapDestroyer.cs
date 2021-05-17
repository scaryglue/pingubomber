using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile wallTile;
    public Tile destructibleTile;

    public GameObject explosionPrefab;

    public void Explode(Vector2 worldPos, int fire)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);

        ExplodeCell(originCell);
        
        //Explosions for the other Cells: if there is a wall in one direction, it should stop exploding in that direction
        for(int i=1; i <= fire; i++)
        {
            if(!ExplodeCell(originCell + new Vector3Int(i, 0, 0)))
            {
                break;
            }
        }

        for (int i = 1; i <= fire; i++)
        {
            if (!ExplodeCell(originCell + new Vector3Int(0, i, 0)))
            {
                break;
            }
        }

        for (int i = 1; i <= fire; i++)
        {
            if (!ExplodeCell(originCell + new Vector3Int(-i, 0, 0)))
            {
                break;
            }
        }

        for (int i = 1; i <= fire; i++)
        {
            if (!ExplodeCell(originCell + new Vector3Int(0, -i, 0)))
            {
                break;
            }
        }
    }

    bool ExplodeCell(Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);

        if(tile == wallTile)
        {
            return false;
        }

        if(tile == destructibleTile)
        {
            //Remove tile
            tilemap.SetTile(cell, null);
        }

        //Create an explosion
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        var explosion = Instantiate(explosionPrefab, pos, Quaternion.identity);
        StartCoroutine(waitForAnimation(explosion));

        return true;
    }

    IEnumerator waitForAnimation(GameObject explosion)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(explosion);
    }
}
