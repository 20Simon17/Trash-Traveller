using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hitpoints : MonoBehaviour
{
    //Player HP = 10
    //litet skr�p hp = 4
    //stort skr�p hp = 7

    //fr�tande skr�p skada = 0,5
    //crossbow skada = 1
    //pistol skada = 1 men snabbare

    //heal 0,5 per 5 sekunder

    //Lista med hela hj�rtan
    //lista med halva hj�rtan
    //checka hur mycket skada man tog / hur mycket hp man har kvar
    //ta bort platser fr�n listan
    //om man kan heala (shouldHeal), l�gg till hj�rta


    //public TextMeshProUGUI playerHPtext;
    public float playerHP = 10;

    //public List<Image> fullHearts;
    //public List<Image> halfHearts;

    public Image[] hearts;
    int heartsArraySpot = 0;

    bool shouldTakeTickDamage = false;
    bool shouldHeal = true;
    bool takeShootingDamage = true;

    public float damageTimer = 1f;
    public float corrosiveTimer = 2.1f;
    public float timeBetweenHeals = 5f;
    public float healCooldown = 10f;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            shouldTakeTickDamage = true;
            shouldHeal = false;
            damageTimer = 1f;
        }

        if(collision.gameObject.layer == 9)
        {
            playerHP -= 1f;
            healCooldown = 10f;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            shouldTakeTickDamage = false;
            shouldHeal = true;

            timeBetweenHeals = 5f;
            damageTimer = 1f;
            corrosiveTimer = 2.1f;
        }
    }

    

    private void Update()
    {
        damageTimer -= Time.deltaTime;
        corrosiveTimer -= Time.deltaTime;
        timeBetweenHeals -= Time.deltaTime;

        //playerHPtext.text = "HP: " + playerHP.ToString();

        if (corrosiveTimer >= 0)
        {
            if(damageTimer <= 0)
            {
                damageTimer = 1f;
                playerHP -= 0.5f;
            }
        }

        if (shouldTakeTickDamage)
        {
            if(damageTimer <= 0f)
            {
                damageTimer = 1f;
                playerHP -= 0.5f;
            }
        }

        if (shouldHeal)
        {
            if(timeBetweenHeals <= 0f)
            {
                timeBetweenHeals = 5f;
                playerHP += 0.5f;
            }
        }

        if(playerHP >= 10)
        {
            shouldHeal = false;
            playerHP = 10f;
        }

        if(healCooldown > 0)
        {
            shouldHeal = false;
        }

        else if(healCooldown <= 0)
        {
            shouldHeal = true;
        }

        heartsArraySpot = 0;

        foreach (var item in hearts)
        {
            item.gameObject.SetActive(false);
        }
        for (float i = 0; i < playerHP; i += 0.5f)
        {
            hearts[heartsArraySpot].gameObject.SetActive(true);
            heartsArraySpot++;

        }
    }
}