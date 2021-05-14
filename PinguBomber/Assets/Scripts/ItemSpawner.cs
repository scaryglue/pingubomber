using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemSpawner : MonoBehaviour
{
    public Tilemap tilemap;

    public GameObject fireItem;
    public GameObject rollerBlade;
    public GameObject bombUp;

    public Tile freeTile;

    public int itemCount = 5;

    public int speedItems = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var position in tilemap.cellBounds.allPositionsWithin)
        {
            if(itemCount == 0)
            {
                return;
            }

            if(tilemap.GetTile<Tile>(position) == null)
            {
                int randomCount = Random.Range(0, 50);

                if(randomCount == 0)
                {
                    Instantiate(fireItem, tilemap.GetCellCenterWorld(position), Quaternion.identity);
                    itemCount--;
                }
                else if(randomCount == 1)
                {
                    if(speedItems <= 2)
                    {
                        Instantiate(rollerBlade, tilemap.GetCellCenterWorld(position), Quaternion.identity);
                        speedItems++;
                        itemCount--;
                    }
                }
                else if(randomCount == 2)
                {
                    Instantiate(bombUp, tilemap.GetCellCenterWorld(position), Quaternion.identity);
                    itemCount--;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(itemCount <= 3)
        {
            Start();
        }
    }
}
