using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitpoints : MonoBehaviour
{
    //Player HP = 10
    //litet skräp hp = 4
    //stort skräp hp = 7

    //frätande skräp skada = 0,5
    //crossbow skada = 1
    //pistol skada = 1 men snabbare

    public Text playerHPtext;

    float playerHP = 10;

    public float damageTimer = 1f;
    public float corrosiveTimer = 2f;

    GameObject player = GameObject.FindGameObjectWithTag("Player");

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            damageTimer = 1f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            if(damageTimer <= 0)
            {
                damageTimer = 1f;
                playerHP -= 0.5f;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        damageTimer = 1f;
        corrosiveTimer = 2f;
    }

    private void Update()
    {
        damageTimer -= Time.deltaTime;
        corrosiveTimer -= Time.deltaTime;

        if(corrosiveTimer >= 0)
        {
            if(damageTimer <= 0)
            {
                damageTimer = 1f;
                playerHP -= 0.5f;
            }
        }

        playerHPtext.text = "HP: " + playerHP.ToString();

        Debug.Log(playerHP);
    }
}
