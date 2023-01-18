using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timemachine : MonoBehaviour
{
    [SerializeField]
    KeyCode Timemachinee;

    public GameObject buttons;

    public void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Timemachine" && Input.GetKey(Timemachinee))
        {
            Debug.Log("hargjort");
            buttons.SetActive(true);
        }
    }
}
