using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu instance;
    public GameObject pause;
    public GameObject gameOver;
    public GameObject UI;
    public bool GameIsPaused = false;
    public bool GameIsOver = false;

    private void Awake()
    {
        instance = this;
    }

    public void PausePressed()
    {
        if (GameIsOver)
            return;

        if (GameIsPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        Time.timeScale = 1f;

        GameIsPaused = false;

        UI.SetActive(true);
        pause.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;

        GameIsPaused = true;

        UI.SetActive(false);
        pause.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        GameIsOver = true;
        GameIsPaused = true;

        UI.SetActive(false);
        gameOver.SetActive(true);
    }

    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene("Menu");
    }

    public void restartGame()
    {
        Resume();
        SceneManager.LoadScene("Game");
    }
}
