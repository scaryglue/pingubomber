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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJoin(PlayerInput pi)
    {
        if(numPlayers == 0)
        {
            //pi.DeactivateInput();
            pi.transform.position = new Vector3(-6.5f, -4.5f, 0f);
            pi.GetComponentInChildren<BombSpawner>().tilemap = thisTilemap;
        }
        else if(numPlayers == 1)
        {
            pi.transform.position = new Vector3(4.5f, 6.5f, 0);
            pi.GetComponentInChildren<BombSpawner>().tilemap = thisTilemap;
            //pi.DeactivateInput();
        }
        players.Add(pi);
        numPlayers++;

        if(numPlayers == 2)
        {
            foreach(PlayerInput player in players)
            {
                //player.ActivateInput();
            }
        }

    }
}
