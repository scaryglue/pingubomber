using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerBlade : MonoBehaviour
{
    public float itemDuration = 10f;
    public float multiplier = 1.5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.moveSpeed *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(itemDuration);

        controller.moveSpeed /= multiplier;

        Destroy(gameObject);
    }
}
