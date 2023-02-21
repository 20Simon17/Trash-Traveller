using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Introduktion : MonoBehaviour
{

    int imageNumber = 0;

    //public Sprite[] bubbles;
    SpriteRenderer rend;
    Image image;

    public Sprite[] bubbles;

    public GameObject imageObject;

    public GameObject button;


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        image = imageObject.GetComponent<Image>(); 
    }

    public void Intro()
    {
        imageObject.SetActive(true);
        image.sprite = bubbles[imageNumber];
        imageNumber += 1;

        if (imageNumber >= bubbles.Length)
        {
            imageNumber = bubbles.Length-1;
            imageObject.SetActive(false);
        }

        rend.sprite = bubbles[imageNumber];
    }
}
