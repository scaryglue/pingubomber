using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenEnter : MonoBehaviour
{
    private GameManage gameManager;
    private int winnerNumber = 0;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManage>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        winnerNumber = (collision.gameObject.GetComponent<PlayerController>().playerNumber % 2) + 1;
        gameManager.winnerWon(winnerNumber);
    }
}
