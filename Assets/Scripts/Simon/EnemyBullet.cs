using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rb;
    Player player;

    public float moveSpeed;
    Vector2 moveDirection;

    float projectileAliveTime = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();

        moveDirection = (player.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = moveDirection;

        transform.LookAt(player.transform);
        transform.Rotate(new Vector3(0, 90, 0));
    }

    private void Update()
    {
        projectileAliveTime += Time.deltaTime;

        if(projectileAliveTime >= 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            Destroy(gameObject);
        }
    }
}