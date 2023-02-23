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

        moveDirection = (player.transform.position - transform.position).normalized * moveSpeed; //kollar riktningen skottet måste åka för att träffa spelaren, sker bara 1 gång vilket är när skottet spawnas
        rb.velocity = moveDirection;

        transform.LookAt(player.transform); //roterar skottet mot spelaren
        transform.Rotate(new Vector3(0, 90, 0)); //roterar skottet med 90 grader på y axeln eftersom det av någon anledning låg platt annars.. (osynligt)
    }

    private void Update()
    {
        projectileAliveTime += Time.deltaTime;

        if(projectileAliveTime >= 2) //om skottet existerat i mer än eller lika med 2 sekunder så tar vi bort det
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7) //om skottet kolliderar med spelaren så tar vi bort skottet
        {
            Destroy(gameObject);
        }
    }
}