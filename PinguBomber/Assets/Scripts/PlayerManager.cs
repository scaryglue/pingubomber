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

    public GameObject Player1Join;
    public GameObject Player2Join;

    public GameObject Countdown3;
    public GameObject Countdown2;
    public GameObject Countdown1;
    public GameObject CountdownGO;

    public GameObject CountdownBackground;

    public Vector3Int basePosition;

    public int mapSize;

    public Sprite ProsiebenSprite;

    private bool random;

    void Start() {
        basePosition = thisTilemap.origin;
        if(FindObjectOfType<MapCreator>() != null) {
            mapSize = FindObjectOfType<MapCreator>().size;
            random = true;
        }
        else {
            random = false;
        }
    }

    public void OnJoin(PlayerInput pi)
    {
        if(numPlayers == 0)
        {
            pi.DeactivateInput();
            if(random) {
                pi.transform.position = thisTilemap.GetCellCenterWorld(thisTilemap.WorldToCell(new Vector3(basePosition.x + 1, basePosition.y + 1, 0f)));
            }
            else {
                pi.transform.position = new Vector3(-6.5f, -4.5f, 0f);
            }
            pi.GetComponent<BombSpawner>().tilemap = thisTilemap;
            pi.GetComponent<PlayerController>().playerNumber = 1;
            pi.GetComponent<PlayerController>().tilemap = thisTilemap;

            if(GlobalVariables.ProsiebenActivated) {
                pi.GetComponentInChildren<SpriteRenderer>().sprite = ProsiebenSprite;
            }

            Player1Join.SetActive(false);
        }
        else if(numPlayers == 1)
        {
            pi.DeactivateInput();
            if(random) {
                pi.transform.position = new Vector3(basePosition.x + mapSize - 1, basePosition.y + mapSize - 1, 0);
            }
            else {
                pi.transform.position = new Vector3(4.5f, 6.5f, 0);
            }
            pi.GetComponent<BombSpawner>().tilemap = thisTilemap;
            pi.GetComponent<PlayerController>().playerNumber = 2;
            pi.GetComponent<PlayerController>().tilemap = thisTilemap;
            Player2Join.SetActive(false);
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
        CountdownBackground.SetActive(true);
        Countdown3.SetActive(true);

        yield return new WaitForSeconds(1f);

        Countdown3.SetActive(false);

        Countdown2.SetActive(true);
        yield return new WaitForSeconds(1f);

        Countdown2.SetActive(false);

        Countdown1.SetActive(true);
        yield return new WaitForSeconds(1f);

        Countdown1.SetActive(false);


        CountdownGO.SetActive(true);
        CountdownBackground.SetActive(false);



        foreach (PlayerInput player in players)
        {
            player.ActivateInput();
        }

        yield return new WaitForSeconds(0.5f);
        CountdownGO.SetActive(false);
    }

    public void DisableInputs()
    {
        foreach (PlayerInput player in players)
        {
            player.DeactivateInput();
        }
    }
}
