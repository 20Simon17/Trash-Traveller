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

    [SerializeField]
    KeyCode Timemachinee;

    public GameObject buttons;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); //referens till animatorn på spelaren -Simon
        numberOfTaggedObjects = GameObject.FindGameObjectsWithTag("Trash").Length;
        //hittar objectsen med tagen Trash
    }

    void Update()
    {
        if (Input.GetKey(left) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Running", true); //när man håller ner vänster rörelse knapp + spring knappen så ska spring animationen kunna köras -Simon
            anim.SetBool("Walking", false); //-||- så ska gå animationen inte kunna köras -Simon

            transform.position -= new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = false;
            //håll in Lskift för att springa snabbare tillsammans med vanliga left knappen.

        }

        else if (Input.GetKey(left))
        {
            anim.SetBool("Running", false); //när man håller ner vänster rörelse knapp så ska spring animationen inte kunna spelas -Simon
            anim.SetBool("Walking", true); //-||- så ska gå animationen kunna spelas -Simon

            transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = false;
            //movement åt vänster:)
        }

        if (Input.GetKey(right) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Running", true); //när man håller ner höger rörelse knapp + spring knappen så ska spring animationen kunna köras  -Simon
            anim.SetBool("Walking", false); //-||- så ska gå animationen inte kunna köras -Simon

            transform.position += new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = true;
            //håll in Lskift för att springa snabbare tillsammans med vanliga right knappen.
        }

        else if (Input.GetKey(right))
        {
            anim.SetBool("Running", false); //när man håller ner höger rörelse knapp så ska spring animationen inte kunna spelas -Simon
            anim.SetBool("Walking", true); //-||- så ska gå animationen kunna spelas -Simon

            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rend.flipX = true;
            //movement åt höger:)
        }

        if (Input.GetKeyDown(jump) && grounded)
        {
            anim.SetBool("Running", false); //när man är på marken och trycker på hopp knappen så ska inte spring animationen kunna spelas längre  -Simon
            anim.SetBool("Walking", false); //-||- så ska inte gå animationen kunna spelas längre -Simon
            anim.Play("Jump"); // -||- så ska hopp animationen spelas  -Simon

            rb.velocity = Vector2.up * JumpVelocity;
            //hittar rb för att hoppa
        }

        if(!Input.GetKey(right) && !Input.GetKey(left)) //om man varken håller höger eller vänster rörelse knapp... -Simon 
        {
            anim.SetBool("Running", false); //så ska spring animationen inte kunna spelas -Simon
            anim.SetBool("Walking", false); //så ska gå animationen inte kunna spelas -Simon
        }

        UpdatenumberOfTaggedObjects = GameObject.FindGameObjectsWithTag("Trash").Length;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {  
            grounded = true;
        }
        //den här och den under är det som gör att man bara kan hoppa när man är på layer 6
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            grounded = false;
        }
        // -||-
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        print((UpdatenumberOfTaggedObjects / numberOfTaggedObjects) * 100 % 10f);
        
        if (collision.gameObject.tag == "Trash")
        {

            if (((UpdatenumberOfTaggedObjects / numberOfTaggedObjects) * 100) % 10f == 5)
            {
                trashList[trashbar].gameObject.SetActive(false);
                trashbar--;

                if (trashbar <= 0)
                {
                    trashbar = 0;
                }
            }
           //den här koden kopplar ihop hur många object med tagen trash från början till hur många det finns kvar genom att dela och sen gångra med höger för att den ska ta bort en plats i listan för trashbaren när 10 % försvunnit och sen 20% och så vidare
        }

        if (collision.gameObject.layer == 6) //när spelaren landar på marken (lager 6) så kan land animationen spelas -Simon
        {
            anim.SetBool("Land", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6) //om spelaren lämnar marken (lager 6) så kan land animationen inte spelas längre -Simon
        {
            anim.SetBool("Land", false);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Timemachine" && Input.GetKey(Timemachinee) && UpdatenumberOfTaggedObjects == 0)
        {
            Debug.Log("hargjort");
            buttons.SetActive(true);
        }

        //det här gör så att timemachine canvasen kommer upp när man klickar E men bara om man har städat klart alltså att det inte finns några taggade objects kvar i scenen.
    }
}
