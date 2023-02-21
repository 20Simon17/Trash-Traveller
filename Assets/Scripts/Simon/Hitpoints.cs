using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Hitpoints : MonoBehaviour
{
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

    public float damageTimer;
    public float corrosiveTimer;
    public float timeBetweenHeals;
    public float healCooldown;
    public float blinkTimer = 0.3f;

    float damageTimerOriginal;
    float corrosiveTimerOriginal;
    float timeBetweenHealsOriginal;
    float healCooldownOriginal;
    float blinkTimerOriginal;

    public Animator anim;
    public SpriteRenderer rend;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();

        damageTimerOriginal = damageTimer;
        corrosiveTimerOriginal = corrosiveTimer;
        corrosiveTimer = 0;
        timeBetweenHealsOriginal = timeBetweenHeals;
        healCooldownOriginal = healCooldown;
        blinkTimerOriginal = blinkTimer;

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
            damageTimer = damageTimerOriginal;
        }

        if(collision.gameObject.layer == 9)
        {
            playerHP -= 1f;
            healCooldown = healCooldownOriginal;
            Destroy(collision.gameObject);
            characterDamageBlink();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            shouldTakeTickDamage = false;
            shouldHeal = true;

            timeBetweenHeals = timeBetweenHealsOriginal;
            damageTimer = damageTimerOriginal;
            corrosiveTimer = corrosiveTimerOriginal;
            healCooldown = healCooldownOriginal;
        }
    }

    private void Update()
    {
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

        if(blinkTimer <= 0)
        {
            rend.color = Color.white;
        }
       
        damageTimer -= Time.deltaTime;
        corrosiveTimer -= Time.deltaTime;
        timeBetweenHeals -= Time.deltaTime;
        healCooldown -= Time.deltaTime;
        blinkTimer -= Time.deltaTime;

        if (corrosiveTimer >= 0)
        {
            if(damageTimer <= 0)
            {
                damageTimer = damageTimerOriginal;
                playerHP -= 0.5f;
                healCooldown = healCooldownOriginal;
                characterDamageBlink();
            }
        }

        if (shouldTakeTickDamage)
        {
            if(damageTimer <= 0f)
            {
                damageTimer = damageTimerOriginal;
                playerHP -= 0.5f;
                healCooldown = healCooldownOriginal;
                characterDamageBlink();
            }
        }

        if (shouldHeal)
        {
            if(timeBetweenHeals <= 0f)
            {
                timeBetweenHeals = timeBetweenHealsOriginal;
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

        if(playerHP <= 0 && !hasDied)
        {
            hasDied = true;
            anim.SetBool("Death", true);
            shouldHeal = false;
        }
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void characterDamageBlink()
    {
        rend.color = new Color32(255, 150, 150, 255);
        blinkTimer = blinkTimerOriginal;
    }
}