using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    private int numPlayers = 0;
    public Tilemap thisTilemap;

    private List<PlayerInput> players = new List<PlayerInput>();

    public void OnJoin(PlayerInput pi)
    {
        if(numPlayers == 0)
        {
            pi.DeactivateInput();
            pi.transform.position = new Vector3(-6.5f, -4.5f, 0f);
            pi.GetComponent<BombSpawner>().tilemap = thisTilemap;
            pi.GetComponent<PlayerController>().playerNumber = 1;
            pi.GetComponent<PlayerController>().tilemap = thisTilemap;
        }
        else if(numPlayers == 1)
        {
            pi.DeactivateInput();
            pi.transform.position = new Vector3(4.5f, 6.5f, 0);
            pi.GetComponent<BombSpawner>().tilemap = thisTilemap;
            pi.GetComponent<PlayerController>().playerNumber = 2;
            pi.GetComponent<PlayerController>().tilemap = thisTilemap;
        }
        players.Add(pi);
        numPlayers++;

        if(numPlayers == 2)
        {
            StartCoroutine(waitTilBeginning());
        }

    }

    IEnumerator waitTilBeginning()
    {
        yield return new WaitForSeconds(1f);
        foreach (PlayerInput player in players)
        {
            player.ActivateInput();
        }
    }

    public void DisableInputs()
    {
        foreach (PlayerInput player in players)
        {
            player.DeactivateInput();
        }
    }
}
