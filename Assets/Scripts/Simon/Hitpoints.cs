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

        //v�rdena h�r under �r original v�rden f�r alla timers s� att vi l�tt kan starta om de
        damageTimerOriginal = damageTimer;
        corrosiveTimerOriginal = corrosiveTimer;
        corrosiveTimer = 0;
        timeBetweenHealsOriginal = timeBetweenHeals;
        healCooldownOriginal = healCooldown;
        blinkTimerOriginal = blinkTimer;

        for (int x = 0; x < 20; x++) //refererar till alla 20 objekt som tillsammans bildar hp baren n�r man startar spelet och ger alla en egen plats i en array
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
        if (collision.gameObject.layer == 8) //om spelaren kolliderar med n�got p� skr�p lagret s� startar vi timern f�r att ta skada
        {
            shouldTakeTickDamage = true;
            shouldHeal = false;
            damageTimer = damageTimerOriginal;
        }

        if(collision.gameObject.layer == 9) //om spelaren kolliderar med n�got p� skott lagret s� tar spelaren 1 skada, heal timern startas om och skottet f�rsvinner
        {
            playerHP -= 1f;
            healCooldown = healCooldownOriginal;
            Destroy(collision.gameObject);
            characterDamageBlink();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8) //om spelaren slutar kollidera med n�got p� skr�p lagret s� �ndrar vi v�rdena p� massor av variabler 
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
        for (float i = 0; i < playerHP; i += 0.5f) //s�tter hj�rtan till aktiva s� l�nge hp variabelns v�rde �r stort nog f�r att n� deras plats i hearts arrayen
        {
            hearts[heartsArraySpot].gameObject.SetActive(true);
            heartsArraySpot++;
        }

        if(blinkTimer <= 0) //om spelaren inte l�ngre ska blinka s� s�tter vi tillbaka f�rgen till ursprungsf�rgen
        {
            rend.color = Color.white;
        }
       
        damageTimer -= Time.deltaTime;
        corrosiveTimer -= Time.deltaTime;
        timeBetweenHeals -= Time.deltaTime;
        healCooldown -= Time.deltaTime;
        blinkTimer -= Time.deltaTime;

        if (corrosiveTimer >= 0) //s� l�nge corrosive timer �r �ver 0
        {
            if(damageTimer <= 0) //s� kollar vi om damage timer g�r under 0 f�r att spelaren ska ta skada
            {
                damageTimer = damageTimerOriginal;
                playerHP -= 0.5f;
                healCooldown = healCooldownOriginal;
                characterDamageBlink();
            }
        }

        if (shouldTakeTickDamage) //om spelaren ska ta tick damage (allts� om den st�r p� skr�p)
        {
            if(damageTimer <= 0f) //s� kollar vi om damage timer g�r under 0 f�r att spelaren ska ta skada 
            {
                damageTimer = damageTimerOriginal;
                playerHP -= 0.5f;
                healCooldown = healCooldownOriginal;
                characterDamageBlink();
            }
        }

        if (shouldHeal) //om man ska kunna heala
        {
            if(timeBetweenHeals <= 0f) //s� kollar vi om timeBetweenHeals �r mindre �n eller lika med 0 f�r att spelaren ska f� lite hp
            {
                timeBetweenHeals = timeBetweenHealsOriginal;
                playerHP += 0.5f;
            }
        }

        if(playerHP >= 10) //om spelarens hp �r �ver eller lika med 10 s� s�tter vi tillbaka till 10
        {
            shouldHeal = false;
            playerHP = 10f;
        }

        if(healCooldown > 0) //om healCooldown �r �ver 0 s� ska man inte kunna heala
        {
            shouldHeal = false;
        }

        else if(healCooldown <= 0 && !hasDied) //om healCooldown �r mindre �n eller lika med 0 och man inte har d�tt s� ska man kunna heala
        {
            shouldHeal = true;
        }

        if(playerHP <= 0 && !hasDied) //om spelarens hp �r mindre �n eller lika med 0 och man inte har d�tt s� d�r man
        {
            hasDied = true;
            anim.SetBool("Death", true);
            shouldHeal = false;
        }
    }

    void BackToMenu() //funktion f�r att skicka spelaren tillbaka till menyn
    {
        SceneManager.LoadScene("Menu");
    }

    void characterDamageBlink() //funktion som g�r s� att karakt�ren blinkar r�tt n�r den tar skada
    {
        rend.color = new Color32(255, 150, 150, 255);
        blinkTimer = blinkTimerOriginal;
    }
}