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

    [SerializeField] private AudioSource jumpSound;

    [SerializeField]
    KeyCode Timemachinee;

    public GameObject buttons;

    public bool shouldMove = true;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); //referens till animatorn p� spelaren -Simon
        numberOfTaggedObjects = GameObject.FindGameObjectsWithTag("Trash").Length;

        UpdatenumberOfTaggedObjects = GameObject.FindGameObjectsWithTag("Trash").Length;

        //hittar objectsen med tagen Trash

    }
        void Update()
        {
            UpdatenumberOfTaggedObjects = GameObject.FindGameObjectsWithTag("Trash").Length;

            if (Input.GetKey(left) && Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Running", true); //n�r man h�ller ner v�nster r�relse knapp + spring knappen s� ska spring animationen kunna k�ras -Simon
                anim.SetBool("Walking", false); //-||- s� ska g� animationen inte kunna k�ras -Simon

                transform.position -= new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rend.flipX = false;
                //h�ll in Lskift f�r att springa snabbare tillsammans med vanliga left knappen.

            }

            else if (Input.GetKey(left))
            {
                anim.SetBool("Running", false); //n�r man h�ller ner v�nster r�relse knapp s� ska spring animationen inte kunna spelas -Simon
                anim.SetBool("Walking", true); //-||- s� ska g� animationen kunna spelas -Simon

                transform.position -= new Vector3(speed, 0, 0) * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rend.flipX = false;
                //movement �t v�nster:)
            }

            if (Input.GetKey(right) && Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Running", true); //n�r man h�ller ner h�ger r�relse knapp + spring knappen s� ska spring animationen kunna k�ras  -Simon
                anim.SetBool("Walking", false); //-||- s� ska g� animationen inte kunna k�ras -Simon

                transform.position += new Vector3(sprintspeed, 0, 0) * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rend.flipX = true;
                //h�ll in Lskift f�r att springa snabbare tillsammans med vanliga right knappen.
            }

            else if (Input.GetKey(right))
            {
                anim.SetBool("Running", false); //n�r man h�ller ner h�ger r�relse knapp s� ska spring animationen inte kunna spelas -Simon
                anim.SetBool("Walking", true); //-||- s� ska g� animationen kunna spelas -Simon

                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rend.flipX = true;
                //movement �t h�ger:)
            }

            if (Input.GetKeyDown(jump) && grounded)
            {
                anim.SetBool("Running", false); //n�r man �r p� marken och trycker p� hopp knappen s� ska inte spring animationen kunna spelas l�ngre  -Simon
                anim.SetBool("Walking", false); //-||- s� ska inte g� animationen kunna spelas l�ngre -Simon
                anim.Play("Jump"); // -||- s� ska hopp animationen spelas  -Simon

                rb.velocity = Vector2.up * JumpVelocity;
                //hittar rb f�r att hoppa
                jumpSound.Play();
            }

            if (!Input.GetKey(right) && !Input.GetKey(left)) //om man varken h�ller h�ger eller v�nster r�relse knapp... -Simon 
            {
                anim.SetBool("Running", false); //s� ska spring animationen inte kunna spelas -Simon
                anim.SetBool("Walking", false); //s� ska g� animationen inte kunna spelas -Simon
            
        }
        for (int i = 0; i < trashList.Count; i++) //kod f�r att trashbaren ska ladda in alla 10 delar av baren
        {
            int _index = 10 - i;

            if (i < 0)
            {
                i = 0;
            }

            if (i < UpdatenumberOfTaggedObjects) //det h�r st�nger av trashlisten om UpdatenumberOfTaggedObjects �r mindre �n i
            {
                trashList[i].enabled = true;
            }
            else
            {
                trashList[i].enabled = false;
            }
        }

        if (UpdatenumberOfTaggedObjects == 0) //och den h�r st�nger av trashbar objectet om UpdatenumberOfTaggedObjects = 0
        {
                trashList[trashbar].gameObject.SetActive(false);
                trashbar--;

                if (trashbar <= 0) //den h�r s�ger till att om trashbar �r mindre �n 0 �r den alltid 0
                {
                    trashbar = 0;
                }

                //den h�r koden kopplar ihop hur m�nga object med tagen trash fr�n b�rjan till hur m�nga det finns kvar genom att dela och sen g�ngra med h�ger f�r att den ska ta bort en plats i listan f�r trashbaren n�r 10 % f�rsvunnit och sen 20% och s� vidare
            }

            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 6)
            {
                grounded = true;
            }
            //den h�r och den under �r det som g�r att man bara kan hoppa n�r man �r p� layer 6
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
            if (collision.gameObject.layer == 6) //n�r spelaren landar p� marken (lager 6) s� kan land animationen spelas -Simon
            {
                anim.SetBool("Land", true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 6) //om spelaren l�mnar marken (lager 6) s� kan land animationen inte spelas l�ngre -Simon
            {
                anim.SetBool("Land", false);
            }
        }

    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Timemachine" && Input.GetKey(Timemachinee) && UpdatenumberOfTaggedObjects == 0)
        {
            buttons.SetActive(true);
        }

     }
        //det h�r g�r s� att timemachine canvasen kommer upp n�r man klickar E men bara om man har st�dat klart allts� att det inte finns n�gra taggade objects kvar i scenen.
}
