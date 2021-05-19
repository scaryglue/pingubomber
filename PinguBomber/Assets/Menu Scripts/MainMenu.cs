using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void changeVolume(float value)
    {
        mixer.SetFloat("masterVol", Mathf.Log10(value) * 20);
    }

    public void PlayRandomGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
