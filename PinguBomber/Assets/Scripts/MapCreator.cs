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


    void Awake()
    {
        var backGroundTilemap = new GameObject("BackGround");
        var bgtm = backGroundTilemap.AddComponent<Tilemap>();
        var tr = backGroundTilemap.AddComponent<TilemapRenderer>();

        bgtm.tileAnchor = new Vector3(0.5f, 0.5f, 0);
        backGroundTilemap.transform.SetParent(grid.transform);
        tr.sortingLayerName = "Main";


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
        
        gptm.tileAnchor = new Vector3(0, 0, 0);
        GamePlayTilemap.transform.SetParent(grid.transform);
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


        var tmCollider = GamePlayTilemap.AddComponent<TilemapCollider2D>();
        var composite = GamePlayTilemap.AddComponent<CompositeCollider2D>();

        GamePlayTilemap.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

    }
}
