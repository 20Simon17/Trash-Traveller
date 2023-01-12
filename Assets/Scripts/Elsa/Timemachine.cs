using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timemachine : MonoBehaviour
{
    [SerializeField]
    KeyCode Timemachinee;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Timemachine" && Input.GetKey(Timemachinee))
        {
            SceneManager.LoadScene(5);
        }
    }
}
