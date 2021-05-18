using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class BombSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject bombPrefab;
    public float bombSize;
    public float coolDown = 3f;

    private bool bombed = false;
    private Vector3 alreadyBombed;

    // Update is called once per frame
    void Update()
    {
        if(bombed)
        {
            bombSize = gameObject.GetComponent<PlayerController>().bombSize;
            if(bombSize >= 1)
            {
                Vector3 worldPos = transform.position;
                Vector3Int cell = tilemap.WorldToCell(worldPos);
                Vector3 cellCenter = tilemap.GetCellCenterWorld(cell);

                if (alreadyBombed.Equals(cellCenter))
                    return;

                alreadyBombed = cellCenter;

                var newBomb = Instantiate(bombPrefab, cellCenter, Quaternion.identity);

                newBomb.GetComponent<Bomb>().fire = gameObject.GetComponent<PlayerController>().fire;

                bombSize--;
                gameObject.GetComponent<PlayerController>().bombSize = bombSize;

                StartCoroutine(waitForCooldown());
            }
        }
    }

    public void OnBomb(InputAction.CallbackContext context)
    {
        bombed = context.action.triggered;
    }

    IEnumerator waitForCooldown()
    {
        yield return new WaitForSeconds(coolDown);
        gameObject.GetComponent<PlayerController>().bombSize++;
        Debug.Log(gameObject.GetComponent<PlayerController>().bombSize);
    }
}
