using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timetraveller : MonoBehaviour
{
    
    public void Medieval()
    {
        SceneManager.LoadScene(0);
    }
    public void NewYork()
    {

        SceneManager.LoadScene(1);
    }

    public void Future()
    { 
        SceneManager.LoadScene(2);
    }

    public void FutureClean()
    {
        SceneManager.LoadScene(5);
    }

    public void MedievalClean()
    {
        SceneManager.LoadScene(3);
    }

    public void NewYorkClean()
    {
        SceneManager.LoadScene(4);
    }

    public void TheFutureHalfClean()
    {
        SceneManager.LoadScene(6);
    }
    public void MedievalHalfClean()
    {
        SceneManager.LoadScene(7);
    }
}
