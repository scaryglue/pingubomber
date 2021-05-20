using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCreator : MonoBehaviour
{

    public int size;
    public Tile backgroundTile;
    public Tile wallTile;
    public Tile dirtTile;
    public Tilemap background;
    public Tilemap GamePlay;

    public GameObject grid;

    public const int dirt_tiles = 30;
    public int number_dirt = 0;


    void Awake()
    {
        var backGroundTilemap = new GameObject("BackGround");
        var bgtm = backGroundTilemap.AddComponent<Tilemap>();
        var tr = backGroundTilemap.AddComponent<TilemapRenderer>();

        bgtm.tileAnchor = new Vector3(0.5f, 0.5f, 0);
        backGroundTilemap.transform.SetParent(grid.transform);
        tr.sortingLayerName = "Main";
        backGroundTilemap.layer = 7;

        bgtm.origin = new Vector3Int((-size/2),(-size/2) + 1, 0);

        Vector3Int basePosition = bgtm.origin;

        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                bgtm.SetTile(new Vector3Int(basePosition.x + i, basePosition.y + j, 0), backgroundTile);
            }
        }

        var GamePlayTilemap = new GameObject("GamePlay");
        var gptm = GamePlayTilemap.AddComponent<Tilemap>();
        var gptr = GamePlayTilemap.AddComponent<TilemapRenderer>();
        
        gptm.tileAnchor = new Vector3(0.5f, 0.5f, 0);
        GamePlayTilemap.transform.SetParent(grid.transform);
        GamePlayTilemap.layer = 6;
        gptr.sortingLayerName = "Main";

        gptm.origin = new Vector3Int((-size/2),(-size/2) + 1, 0);

        basePosition = bgtm.origin;

        //Add outer walls
        for(int i=0; i < size; i++) {
            gptm.SetTile(new Vector3Int(basePosition.x + i, basePosition.y, 0), wallTile);
            gptm.SetTile(new Vector3Int(basePosition.x + i, basePosition.y + size, 0), wallTile);
        }
        for(int i=0; i <= size; i++) {
            gptm.SetTile(new Vector3Int(basePosition.x, basePosition.y + i, 0), wallTile);
            gptm.SetTile(new Vector3Int(basePosition.x + size, basePosition.y + i, 0), wallTile);
        }

        //Add inner wallblocks
        for(int i=4; i <= size; i += 4) {
            for(int j=4; j <= size; j +=4) {
                gptm.SetTile(new Vector3Int(basePosition.x + i, basePosition.y + j, 0), wallTile);
            }
        }


        //Setting the dirt:
        for(int i=2; i < size; i++) {
            for(int j=2; j < size; j++) {
                if(Random.Range(0,10) == 5 && number_dirt <= dirt_tiles) {
                    gptm.SetTile(new Vector3Int(basePosition.x + i, basePosition.y + j, 0), dirtTile);
                    number_dirt++;
                }
            }
        }

        //Assigning the newly created Tilemap to every component that needs it

        GameObject.Find("Grid").GetComponent<MapDestroyer>().tilemap = gptm;
        GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>().tilemap = gptm;
        GameObject.Find("PlayerManager").GetComponent<PlayerManager>().thisTilemap = gptm;

        var tmCollider = GamePlayTilemap.AddComponent<TilemapCollider2D>();
        var composite = GamePlayTilemap.AddComponent<CompositeCollider2D>();

        tmCollider.usedByComposite = true;
        GamePlayTilemap.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

    }
}
