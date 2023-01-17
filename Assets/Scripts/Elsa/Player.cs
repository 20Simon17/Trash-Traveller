using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float sprintspeed;

    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode jump = KeyCode.Space;

    public Rigidbody2D rb;

    public bool grounded;

    public int JumpVelocity;

    public int trashbar = 9;

    public List<Image> trashList;

    public Animator anim;

    public SpriteRenderer rend;

    public float numberOfTaggedObjects;

    public float UpdatenumberOfTaggedObjects;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        numberOfTaggedObjects = GameObject.FindGameObjectsWithTag("Trash").Length;
    }

    void Update()
    {
        anim.SetFloat("SpeedY", rb.velocity.y);

        if (Input.GetKey(left) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Running", true);
            
            transform.position -= new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = false;

        }

        else if (Input.GetKey(left))
        {
            anim.SetBool("Running", false);
            //walk

            transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = false;
        }

        if (Input.GetKey(right) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Running", true);

            transform.position += new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = true;
        }

        else if (Input.GetKey(right))
        {
            anim.SetBool("Running", false);
            //walk

            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = true;
        }

        if (Input.GetKeyDown(jump) && grounded)
        {
            anim.SetBool("Running", false);
            anim.Play("Jump");

            rb.velocity = Vector2.up * JumpVelocity;
        }

        if(!Input.GetKey(right) && !Input.GetKey(left))
        {
            anim.SetBool("Running", false);
        }

        UpdatenumberOfTaggedObjects = GameObject.FindGameObjectsWithTag("Trash").Length;
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

        print((UpdatenumberOfTaggedObjects / numberOfTaggedObjects) * 100 % 10f);
        
        if (collision.gameObject.tag == "Trash")
        {
            Destroy(collision.gameObject);

            if (((UpdatenumberOfTaggedObjects / numberOfTaggedObjects) * 100) % 10f == 5)
            {
                trashList[trashbar].gameObject.SetActive(false);
                trashbar--;

                if (trashbar <= 0)
                {
                    trashbar = 0;
                }
            }
        }

        if (collision.gameObject.layer == 6)
        {
            anim.SetBool("Land", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            anim.SetBool("Land", false);
        }
    }
}
