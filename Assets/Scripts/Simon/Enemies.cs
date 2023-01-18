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
    }
}
