using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private bool loopMusicPlaying = false;

    private void Start()
    {
        AudioManager.instance.StopAllSound();
        AudioManager.instance.Play("Menu");
    }

    private void Update()
    {
        if (!loopMusicPlaying && !AudioManager.instance.IsPlaying("Menu"))
        {
            loopMusicPlaying = true;
            AudioManager.instance.Play("MenuLoop");
        }
    }

    private void OnDestroy()
    {
        AudioManager.instance.StopAllSound();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
