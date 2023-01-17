using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausMenu : MonoBehaviour
{
    [SerializeField]
    KeyCode paus;

    public GameObject Paus;

    public void Update()
    {
        if (Input.GetKey(paus))
        {
            Paus.SetActive(true);
        }
    }
}
