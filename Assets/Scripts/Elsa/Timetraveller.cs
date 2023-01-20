using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timetraveller : MonoBehaviour
{
    
    public void Medieval()
    {
        SceneManager.LoadScene("Medieval");
    }
    public void NewYork()
    {

        SceneManager.LoadScene("New York");
    }

    public void Future()
    { 
        SceneManager.LoadScene("The Future");
    }

    public void FutureClean()
    {
        SceneManager.LoadScene("The Future clean");
    }

    public void MedievalClean()
    {
        SceneManager.LoadScene("Medieval clean");
    }

    public void NewYorkClean()
    {
        SceneManager.LoadScene("New York clean");
    }

    public void TheFutureHalfClean()
    {
        SceneManager.LoadScene("The Future half clean");
    }
    public void MedievalHalfClean()
    {
        SceneManager.LoadScene("Medieval half clean");
    }

    //knaparna till timetravell canvasen (referar till player canvas timemachinee)
}
