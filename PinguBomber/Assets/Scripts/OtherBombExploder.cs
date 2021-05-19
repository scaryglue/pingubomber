using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBombExploder : MonoBehaviour
{
    private const int DEFAULT_FIRE = 2;

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Other thing hit");

        if(other.CompareTag("Bomb")) {
            FindObjectOfType<MapDestroyer>().Explode(other.gameObject.transform.position, DEFAULT_FIRE);
        }
    }
}
