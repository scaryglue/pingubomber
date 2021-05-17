using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class BombSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject bombPrefab;
    public float bombSize;
    public float coolDown = 5f;

    private bool bombed = false;


    // Update is called once per frame
    void Update()
    {
        if(bombed)
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

    public void OnBomb(InputAction.CallbackContext context)
    {
        bombed = context.action.triggered;
    }

    IEnumerator waitForCooldown()
    {
        yield return new WaitForSeconds(coolDown);

        gameObject.GetComponentInParent<PlayerController>().bombSize++;
    }
}
