using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float sprintspeed;

    [SerializeField]
    KeyCode left, right, jump;

    public Rigidbody2D rigidbody2d;

    public bool grounded;

    public int JumpVelocity;

    public int trashbar = 10;

    public List<Image> trashList;

    public Animator anim;

    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        left = KeyCode.A;
        right = KeyCode.D;
        jump = KeyCode.Space;
    }

    void Update()
    {
        if (Input.GetKey(left) && Input.GetKey(KeyCode.LeftShift))
        {

            transform.position -= new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }

        else if (Input.GetKey(left))
        {
            transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(right) && Input.GetKey(KeyCode.LeftShift))
        {

            transform.position += new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }

        else if (Input.GetKey(right))
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKeyDown(jump) && grounded)
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
