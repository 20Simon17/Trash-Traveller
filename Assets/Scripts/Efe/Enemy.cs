using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour // den här enemy scripten används inte.
{
    public GameObject player; // -Efe

    public float speed; // -Efe

    public float distancebetween; // -Efe

    public int health = 100; // -Efe
   
    private float distance; // -Efe

    public void TakeDamage(int damage) // om health blir 0 dör man -Efe
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    

    void Die() // die = döda gubben -Efe
    {
        Destroy(gameObject);
    }

    private void Update() //  -Efe
    {

        distance = Vector2.Distance(transform.position, player.transform.position);

        Vector2 direction = player.transform.position - transform.position;


        direction.Normalize(); // -Efe

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distancebetween) // -Efe
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
   
}
