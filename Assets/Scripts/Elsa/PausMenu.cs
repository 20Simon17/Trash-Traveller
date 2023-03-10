using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausMenu : MonoBehaviour
{
    public GameObject Paus;

    public static bool GameIsPaused = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    
    public void Resume()
    {
        Paus.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        Paus.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Quittomain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    //tv? voids som startar/pausar spelet och tar fram en canvas
}
