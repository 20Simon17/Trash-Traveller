using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Introduktion : MonoBehaviour
{

    int imageNumber = 0;

    Image image;

    public GameObject smallIntroduction;
    public GameObject gubbe;
    public GameObject introduction;

    public Sprite[] bubbles;
    public Sprite[] bubbles2;
 
    public GameObject imageObject;

    public GameObject button;


    private void Start()
    {
        image = imageObject.GetComponent<Image>();

        bubbles2 = bubbles;
    }

    public void Intro()
    {
        Color bla = image.color;
        bla.a = 1;
        image.color = bla;
        
        imageObject.SetActive(true);
        image.sprite = bubbles[imageNumber];
        imageNumber += 1;


        if (imageNumber >= bubbles.Length)
        {
            imageNumber = bubbles.Length-1;
            imageObject.SetActive(false);

            gubbe.SetActive(false);

            bubbles = bubbles2;
            imageNumber = 0;
            smallIntroduction.SetActive(true);
            introduction.SetActive(false);
        }
    }
}
