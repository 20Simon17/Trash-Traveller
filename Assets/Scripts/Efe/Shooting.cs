using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public bool usingPistol; //för att kolla vilken animation som ska spelas när man skjuter :) -Simon
    public bool usingCrossbow; //för att kolla vilken animation som ska spelas när man skjuter :) -Simon

    public Transform firepoint; // -Efe

    public Transform rotatePoint; // -Efe

    public int damage = 40; // -Efe

    public bool canFire; // -Efe

    private float timer; // -Efe

    public float TimebetweeenFiring; // -Efe

    public LayerMask mask; // -Efe

    public GameObject bullet; // -Efe

    Player player; //Simon

    private void Start() //Simon
    {
        player = FindObjectOfType<Player>(); //hämtar spelar koden i scenen (finns bara 1)
    }

    private void Update() // -Efe
    {
        if (Input.GetMouseButtonDown(0) && canFire) 
        {
            StartCoroutine(Shoot());

            canFire = false;
        }

        if (!canFire) // -Efe
        {
            timer += Time.deltaTime;

            if (timer > TimebetweeenFiring) // -Efe
            {
                canFire = true;

                timer = 0;
            }
        }
    }


    IEnumerator Shoot() // -Efe
    {
        Vector2 dir = -(rotatePoint.position - firepoint.position).normalized; // -Efe

        RaycastHit2D hitinfo = Physics2D.Raycast(firepoint.position, dir, 60f, mask); // -Efe

        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation); // -Efe

        newBullet.transform.up = dir; // -Efe

        yield return new WaitForSeconds(0.02f); // -Efe

        if (usingPistol)
        {
            player.anim.SetTrigger("PistolShoot"); // Simon
        }

        else if (usingCrossbow)
        {
            player.anim.SetTrigger("CrossbowShoot"); // Simon
        }
    }
   
}
