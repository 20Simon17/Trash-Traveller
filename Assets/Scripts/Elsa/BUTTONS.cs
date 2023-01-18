using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BUTTONS : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene (2);
    }

    public void Settings()
    {
        SceneManager.LoadScene(9);
    }

    public void Quit()
    {
        Application.Quit();
    }

    //Menyknappar
}
