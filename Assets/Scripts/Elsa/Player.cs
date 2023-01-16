using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;

    public float sprintspeed;

    [SerializeField]
    KeyCode Left;

    [SerializeField]
    KeyCode Right;

    [SerializeField]
    KeyCode Jump;

    [SerializeField]
    float reload;

    [SerializeField]
    Vector3 direction = new Vector3(1, 0, 0);

    public Rigidbody2D rigidbody2d;

    public bool grounded;

    public int JumpVelocity;

    public int trashbar = 10;

    public List<Image> trashList;

    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        reload += Time.deltaTime;
        if (Physics2D.BoxCast(transform.position, new Vector2(1, 0.25f), 0, -transform.up, 2))
        {
           
        }

        if (Input.GetKey(Left) && Input.GetKey(KeyCode.LeftShift))
        {

            transform.position -= new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }

        else if (Input.GetKey(Left))
        {
            transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(Right) && Input.GetKey(KeyCode.LeftShift))
        {

            transform.position += new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }

        else if (Input.GetKey(Right))
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKeyDown(Jump) && grounded)
        {
            rigidbody2d.velocity = Vector2.up * JumpVelocity;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            grounded = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trash")
        {
            trashList[trashbar].gameObject.SetActive(false);
            trashbar--;

            if (trashbar <= 0)
            {
                trashbar = 0;
            }
        }
    }
}
