using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firepoint;

    public Transform rotatePoint;

    public int damage = 40;

    public bool canFire;

    private float timer;

    public float TimebetweeenFiring;

    public LayerMask mask;

    public GameObject bullet;
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            StartCoroutine (Shoot());

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
        print("shot");

        Vector2 dir = (rotatePoint.position - firepoint.position).normalized;

        RaycastHit2D hitinfo = Physics2D.Raycast(firepoint.position, dir, 60f, mask);

        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);

        newBullet.transform.up = dir;

        if (hitinfo)
        {
            print("raycast hit");
          
            Enemy enemy = hitinfo.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                print("enemy hit");
            }

            
        }

        yield return new WaitForSeconds(0.02f);

    }

}
