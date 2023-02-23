using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float hp;

    public bool modernEnemy;

    public GameObject bullet;

    public float pistolShootDelay;

    float pistolShootDelayOriginal;

    Player player;
    Vector3 playerPos;
    Animator anim;

    float diffrence;

    public SpriteRenderer rend;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();

        pistolShootDelayOriginal = pistolShootDelay;
    }

    private void Update()
    {
        playerPos = player.transform.position;

        if (modernEnemy) //kollar om vi klickat i "modernEnemy" bool, eftersom det beh�ver specifik kod
        {
            pistolShootDelay -= Time.deltaTime;

            if(pistolShootDelay <= 0) //om pistolShootDelay �r mindre �n eller lika med 0 s� ska skr�pet skjuta
            {
                pistolShootDelay = pistolShootDelayOriginal;

                anim.SetTrigger("NYShoot");
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
        else
        {
            anim.SetBool("IsMedievalTrash", true);
        }

        if(hp <= 0) //om skr�pet har mindre �n eller lika med 0 hp s� tar vi bort det
        {
            Destroy(gameObject);
        }

        diffrence = player.transform.position.x - transform.position.x; //kollar vilket h�ll skr�pet ska vara roterat -Elsa

        if (diffrence >= 0) //Elsa
        {
            rend.flipX = true;
        }

        else if (diffrence <= 0) //Elsa
        {
            rend.flipX = false;
        }
    }
}
