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

        moveDirection = (player.transform.position - transform.position).normalized * moveSpeed; //kollar riktningen skottet m�ste �ka f�r att tr�ffa spelaren, sker bara 1 g�ng vilket �r n�r skottet spawnas
        rb.velocity = moveDirection;

        transform.LookAt(player.transform); //roterar skottet mot spelaren
        transform.Rotate(new Vector3(0, 90, 0)); //roterar skottet med 90 grader p� y axeln eftersom det av n�gon anledning l�g platt annars.. (osynligt)
    }

    private void Update()
    {
        projectileAliveTime += Time.deltaTime;

        if(projectileAliveTime >= 2) //om skottet existerat i mer �n eller lika med 2 sekunder s� tar vi bort det
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7) //om skottet kolliderar med spelaren s� tar vi bort skottet
        {
            Destroy(gameObject);
        }
    }
}