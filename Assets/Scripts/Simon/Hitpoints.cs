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

    public bool shouldTakeTickDamage = false;
    public bool shouldHeal = true;
    bool hasDied = false;

    public float damageTimer = 1f;
    public float corrosiveTimer = 2.1f;
    public float timeBetweenHeals = 2f;
    public float healCooldown = 3f;

    public Animator anim;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();

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
            healCooldown = 3f;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            shouldTakeTickDamage = false;
            shouldHeal = true;

            timeBetweenHeals = 2f;
            damageTimer = 1f;
            corrosiveTimer = 2.1f;
            healCooldown = 3f;
        }
    }

    private void Update()
    {
        damageTimer -= Time.deltaTime;
        corrosiveTimer -= Time.deltaTime;
        timeBetweenHeals -= Time.deltaTime;
        healCooldown -= Time.deltaTime;

        if (corrosiveTimer >= 0)
        {
            if(damageTimer <= 0)
            {
                damageTimer = 1f;
                playerHP -= 0.5f;
                healCooldown = 3f;
            }
        }

        if (shouldTakeTickDamage)
        {
            if(damageTimer <= 0f)
            {
                damageTimer = 1f;
                playerHP -= 0.5f;
                healCooldown = 3f;
            }
        }

        if (shouldHeal)
        {
            if(timeBetweenHeals <= 0f)
            {
                timeBetweenHeals = 2f;
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

        else if(healCooldown <= 0 && !hasDied)
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

        if(playerHP <= 0 && !hasDied)
        {
            hasDied = true;
            anim.SetTrigger("Die");
            shouldHeal = false;
            //load death screen?
        }
    }
}