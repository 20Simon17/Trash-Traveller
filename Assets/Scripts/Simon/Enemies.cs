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

        if (modernEnemy)
        {
            pistolShootDelay -= Time.deltaTime;

            if(pistolShootDelay <= 0)
            {
                pistolShootDelay = pistolShootDelayOriginal;

                anim.SetTrigger("NYShoot");
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }

        if(hp <= 0)
        {
            Destroy(gameObject);
        }

        diffrence = player.transform.position.x - transform.position.x;

        if (diffrence >= 0)
        {
            rend.flipX = true;
        }

        else if (diffrence <= 0)
        {
            rend.flipX = false;
        }
    }
}
