using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject bombPrefab;
    public float bombSize;
    public float coolDown = 5f;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            bombSize = gameObject.GetComponentInParent<PlayerController>().bombSize;
            if(bombSize >= 1)
            {
                Vector3 worldPos = transform.position;
                Vector3Int cell = tilemap.WorldToCell(worldPos);
                Vector3 cellCenter = tilemap.GetCellCenterWorld(cell);

                var newBomb = Instantiate(bombPrefab, cellCenter, Quaternion.identity);

                newBomb.GetComponent<Bomb>().fire = gameObject.GetComponentInParent<PlayerController>().fire;

                gameObject.GetComponentInParent<PlayerController>().bombSize = bombSize - 1;

                StartCoroutine(waitForCooldown());
            }
        }
    }

    IEnumerator waitForCooldown()
    {
        yield return new WaitForSeconds(coolDown);

        gameObject.GetComponentInParent<PlayerController>().bombSize++;
    }
}
