using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform playerPos;
    public GameObject bombPrefab;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Vector3 worldPos = playerPos.position;
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            Vector3 cellCenter = tilemap.GetCellCenterWorld(cell);

            Instantiate(bombPrefab, cellCenter, Quaternion.identity);
        }
    }
}
