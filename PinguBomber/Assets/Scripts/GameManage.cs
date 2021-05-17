using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{

    public GameObject winScreen;

    public void winnerWon(int playerNumber)
    {
        winScreen.GetComponentInChildren<TMP_Text>().text = "Player " + playerNumber + " won!";
        winScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
