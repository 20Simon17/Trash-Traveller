using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hitpoints : MonoBehaviour
{
    //litet skräp hp = 4
    //stort skräp hp = 7

    public float playerHP = 10;

    public GameObject[] hearts;
    int heartsArraySpot = 0;

    public GameObject fullHearts;
    public GameObject halfHearts;
    int fH = 0;
    int hH = 0;

    bool shouldTakeTickDamage = false;
    bool shouldHeal = true;

    public float damageTimer = 1f;
    public float corrosiveTimer = 2.1f;
    public float timeBetweenHeals = 5f;
    public float healCooldown = 5f;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        for (int x = 0; x < 20; x++)
        {
            if(hH == fH)
            {
                hearts[x] = halfHearts.transform.GetChild(hH).gameObject;
                hH++;
            }

            else if(hH > fH)
            {
                hearts[x] = fullHearts.transform.GetChild(fH).gameObject;
                fH++;
            }
        }
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
            healCooldown = 5f;
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
            healCooldown = 5f;
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
                healCooldown = 5f;
            }
        }

        if (shouldTakeTickDamage)
        {
            if(damageTimer <= 0f)
            {
                damageTimer = 1f;
                playerHP -= 0.5f;
                healCooldown = 5f;
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
        
        foreach (var item in hearts)
        {
            item.gameObject.SetActive(false);
        }

        heartsArraySpot = 0;
        for (float i = 0; i < playerHP; i += 0.5f)
        {
            hearts[heartsArraySpot].gameObject.SetActive(true);
            heartsArraySpot++;
        }
    }
}