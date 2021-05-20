using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatTester : MonoBehaviour
{
    public AudioSource cell;

    public AudioSource music;

    public void ProsiebenCheat(string input) {
        if(input.Equals("STOPBEINGMUSLIM")) {
            music.Stop();
            GlobalVariables.ProsiebenActivated = true;
            cell.Play();
            StartCoroutine(waitMusic(cell, music));
        }
    }

    IEnumerator waitMusic(AudioSource cell, AudioSource music) {
        while(cell.isPlaying) {
            yield return new WaitForSeconds(1f);
        }

        music.Play();
    }
}
