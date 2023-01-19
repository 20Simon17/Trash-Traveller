using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public bool usingPistol; //för att kolla vilken animation som ska spelas när man skjuter :) -Simon
    public bool usingCrossbow; //för att kolla vilken animation som ska spelas när man skjuter :) -Simon

    public Transform firepoint;

    public Transform rotatePoint;

    public int damage = 40;

    public bool canFire;

    private float timer;

    public float TimebetweeenFiring;

    public LayerMask mask;

    public GameObject bullet;

    Player player; //Simon

    private void Start() //Simon
    {
        player = FindObjectOfType<Player>(); //hämtar spelar koden i scenen (finns bara 1)
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            StartCoroutine(Shoot());

            canFire = false;
        }

        if (!canFire)
        {
            timer += Time.deltaTime;

            if (timer > TimebetweeenFiring)
            {
                canFire = true;

                timer = 0;
            }
        }
    }


    IEnumerator Shoot()
    {
        Vector2 dir = -(rotatePoint.position - firepoint.position).normalized;

        RaycastHit2D hitinfo = Physics2D.Raycast(firepoint.position, dir, 60f, mask);

        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);

        newBullet.transform.up = dir;

        yield return new WaitForSeconds(0.02f);

        if (usingPistol)
        {
            player.anim.SetTrigger("PistolShoot");
        }

        else if (usingCrossbow)
        {
            player.anim.SetTrigger("CrossbowShoot");
        }
    }
}
