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
        anim = GetComponent<Animator>(); //referens till animatorn p� spelaren -Simon
        numberOfTaggedObjects = GameObject.FindGameObjectsWithTag("Trash").Length;
    }

    void Update()
    {
        if (Input.GetKey(left) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Running", true); //n�r man h�ller ner v�nster r�relse knapp + spring knappen s� ska spring animationen kunna k�ras -Simon
            anim.SetBool("Walking", false); //-||- s� ska g� animationen inte kunna k�ras -Simon

            transform.position -= new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = false;

        }

        else if (Input.GetKey(left))
        {
            anim.SetBool("Running", false); //n�r man h�ller ner v�nster r�relse knapp s� ska spring animationen inte kunna spelas -Simon
            anim.SetBool("Walking", true); //-||- s� ska g� animationen kunna spelas -Simon

            transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = false;
        }

        if (Input.GetKey(right) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Running", true); //n�r man h�ller ner h�ger r�relse knapp + spring knappen s� ska spring animationen kunna k�ras  -Simon
            anim.SetBool("Walking", false); //-||- s� ska g� animationen inte kunna k�ras -Simon

            transform.position += new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = true;
        }

        else if (Input.GetKey(right))
        {
            anim.SetBool("Running", false); //n�r man h�ller ner h�ger r�relse knapp s� ska spring animationen inte kunna spelas -Simon
            anim.SetBool("Walking", true); //-||- s� ska g� animationen kunna spelas -Simon

            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = true;
        }

        if (Input.GetKeyDown(jump) && grounded)
        {
            anim.SetBool("Running", false); //n�r man �r p� marken och trycker p� hopp knappen s� ska inte spring animationen kunna spelas l�ngre  -Simon
            anim.SetBool("Walking", false); //-||- s� ska inte g� animationen kunna spelas l�ngre -Simon
            anim.Play("Jump"); // -||- s� ska hopp animationen spelas  -Simon

            rb.velocity = Vector2.up * JumpVelocity;
        }

        if(!Input.GetKey(right) && !Input.GetKey(left)) //om man varken h�ller h�ger eller v�nster r�relse knapp... -Simon 
        {
            anim.SetBool("Running", false); //s� ska spring animationen inte kunna spelas -Simon
            anim.SetBool("Walking", false); //s� ska g� animationen inte kunna spelas -Simon
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

        if (collision.gameObject.layer == 6) //n�r spelaren landar p� marken (lager 6) s� kan land animationen spelas -Simon
        {
            anim.SetBool("Land", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6) //om spelaren l�mnar marken (lager 6) s� kan land animationen inte spelas l�ngre -Simon
        {
            anim.SetBool("Land", false);
        }
    }
}
