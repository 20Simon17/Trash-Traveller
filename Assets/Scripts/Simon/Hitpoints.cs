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

        //värdena här under är original värden för alla timers så att vi lätt kan starta om de
        damageTimerOriginal = damageTimer;
        corrosiveTimerOriginal = corrosiveTimer;
        corrosiveTimer = 0;
        timeBetweenHealsOriginal = timeBetweenHeals;
        healCooldownOriginal = healCooldown;
        blinkTimerOriginal = blinkTimer;

        for (int x = 0; x < 20; x++) //refererar till alla 20 objekt som tillsammans bildar hp baren när man startar spelet och ger alla en egen plats i en array
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
        if (collision.gameObject.layer == 8) //om spelaren kolliderar med något på skräp lagret så startar vi timern för att ta skada
        {
            shouldTakeTickDamage = true;
            shouldHeal = false;
            damageTimer = damageTimerOriginal;
        }

        if(collision.gameObject.layer == 9) //om spelaren kolliderar med något på skott lagret så tar spelaren 1 skada, heal timern startas om och skottet försvinner
        {
            playerHP -= 1f;
            healCooldown = healCooldownOriginal;
            Destroy(collision.gameObject);
            characterDamageBlink();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8) //om spelaren slutar kollidera med något på skräp lagret så ändrar vi värdena på massor av variabler 
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
        for (float i = 0; i < playerHP; i += 0.5f) //sätter hjärtan till aktiva så länge hp variabelns värde är stort nog för att nå deras plats i hearts arrayen
        {
            hearts[heartsArraySpot].gameObject.SetActive(true);
            heartsArraySpot++;
        }

        if(blinkTimer <= 0) //om spelaren inte längre ska blinka så sätter vi tillbaka färgen till ursprungsfärgen
        {
            rend.color = Color.white;
        }
       
        damageTimer -= Time.deltaTime;
        corrosiveTimer -= Time.deltaTime;
        timeBetweenHeals -= Time.deltaTime;
        healCooldown -= Time.deltaTime;
        blinkTimer -= Time.deltaTime;

        if (corrosiveTimer >= 0) //så länge corrosive timer är över 0
        {
            if(damageTimer <= 0) //så kollar vi om damage timer går under 0 för att spelaren ska ta skada
            {
                damageTimer = damageTimerOriginal;
                playerHP -= 0.5f;
                healCooldown = healCooldownOriginal;
                characterDamageBlink();
            }
        }

        if (shouldTakeTickDamage) //om spelaren ska ta tick damage (alltså om den står på skräp)
        {
            if(damageTimer <= 0f) //så kollar vi om damage timer går under 0 för att spelaren ska ta skada 
            {
                damageTimer = damageTimerOriginal;
                playerHP -= 0.5f;
                healCooldown = healCooldownOriginal;
                characterDamageBlink();
            }
        }

        if (shouldHeal) //om man ska kunna heala
        {
            if(timeBetweenHeals <= 0f) //så kollar vi om timeBetweenHeals är mindre än eller lika med 0 för att spelaren ska få lite hp
            {
                timeBetweenHeals = timeBetweenHealsOriginal;
                playerHP += 0.5f;
            }
        }

        if(playerHP >= 10) //om spelarens hp är över eller lika med 10 så sätter vi tillbaka till 10
        {
            shouldHeal = false;
            playerHP = 10f;
        }

        if(healCooldown > 0) //om healCooldown är över 0 så ska man inte kunna heala
        {
            shouldHeal = false;
        }

        else if(healCooldown <= 0 && !hasDied) //om healCooldown är mindre än eller lika med 0 och man inte har dött så ska man kunna heala
        {
            shouldHeal = true;
        }

        if(playerHP <= 0 && !hasDied) //om spelarens hp är mindre än eller lika med 0 och man inte har dött så dör man
        {
            hasDied = true;
            anim.SetBool("Death", true);
            shouldHeal = false;
        }
    }

    void BackToMenu() //funktion för att skicka spelaren tillbaka till menyn
    {
        SceneManager.LoadScene("Menu");
    }

    void characterDamageBlink() //funktion som gör så att karaktären blinkar rött när den tar skada
    {
        rend.color = new Color32(255, 150, 150, 255);
        blinkTimer = blinkTimerOriginal;
    }
}