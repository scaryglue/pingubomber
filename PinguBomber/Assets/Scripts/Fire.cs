using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public AudioSource pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pickupSound.Play();
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.fire++;

        Destroy(gameObject);
    }
}
