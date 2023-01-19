using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pbullet : MonoBehaviour
{
    Enemies enemies; //Simon

    float projectileAliveTime = 0; //Simon

    private void Update()
    {
        projectileAliveTime += Time.deltaTime; //kollar hur l�nge skottet existerat -Simon

        if (projectileAliveTime >= 2) //om skottet existerat i mer �n 2 sekunder
        {
            Destroy(gameObject); //ta bort skottet -Simon
        }

        transform.position += transform.up * Time.deltaTime * 16;
    }

    private void OnTriggerEnter2D(Collider2D collision) //Simon
    {
        if(collision.gameObject.layer == 8) //om skottet kolliderar med ett objekt p� lager 8 (skr�plagret) s� -Simon
        {
            enemies = collision.gameObject.GetComponent<Enemies>(); //h�mtar den enemy koden fr�n objektet -Simon
            enemies.hp -= 1; //tar bort 1 hp fr�n skr�pet -Simon
            Destroy(gameObject); //tar bort skottet -Simon
        }
    }
}
