using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        reload += Time.deltaTime;
        if (Physics2D.BoxCast(transform.position, new Vector2(1, 0.25f), 0, -transform.up, 2))
        {
           
        }

        //Det här är till för att spelaren bara ska kunna hoppa när den rör vid platformarna.
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

}
