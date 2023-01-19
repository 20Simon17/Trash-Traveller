using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    Player player;

    public bool leftWall;
    public bool rightWall;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (leftWall)
        {
            transform.position = new Vector2(player.transform.position.x - 8, 0);
        }

        if (rightWall)
        {
            transform.position = new Vector2(player.transform.position.x + 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            Destroy(collision.gameObject);
        }
    }
}
