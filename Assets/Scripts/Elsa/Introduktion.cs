using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduktion : MonoBehaviour
{

    int introduktion = 0;
    public Sprite[] bubbles;
    SpriteRenderer rend;

    public GameObject button;


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Intro();
    }


    public void Intro()
    {
        rend.sprite = bubbles[introduktion];
        introduktion += 1;

        if (introduktion >= bubbles.Length)
        {
            introduktion = 0;
        }
    }
}
