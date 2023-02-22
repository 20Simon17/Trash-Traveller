using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public GameObject Enter;

    public GameObject Exit;

    Vector3 position = new Vector3(1, 0, 0);

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            Enter.SetActive(true);
        }

        if (collision.gameObject.layer == 15)
        {
            Exit.SetActive(true);
        }

        //det här är vad som startar texten på skärmen när man antingen ska gå upp eller gå ner.
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            Enter.SetActive(false);
        }

        if (collision.gameObject.layer == 15)
        {
            Exit.SetActive(false);
        }

        //det här är vad som stänger av texten på skärmen när man antingen ska gå upp eller gå ner.
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print(collision.name);
        #region Go Up
        if (collision.gameObject.tag == "House 1" && Input.GetKey(KeyCode.E))
        {
            transform.position = new Vector3(-38.8f, 0, 13);
        }

        if (collision.gameObject.tag == "House 2" && Input.GetKey(KeyCode.E))
        {
            transform.position = new Vector3(-35.5f, 1.79f, 13);
        }

        if (collision.gameObject.tag == "House 3" && Input.GetKey(KeyCode.E))
        {
            transform.position = new Vector3(-27.2f, 2.26f, 13);
        }

        if (collision.gameObject.tag == "House 4" && Input.GetKey(KeyCode.E))
        {
            transform.position = new Vector3(-14.9f, 0.06f, 13);
        }

        if (collision.gameObject.tag == "House 5" && Input.GetKey(KeyCode.E))
        {
            print("go up");
            transform.position = new Vector3(-11.4f, 2.4f, 13);
        }

        if (collision.gameObject.tag == "House 6" && Input.GetKey(KeyCode.E))
        {
            transform.position = new Vector3(5.11f, -0.02f, 13);
        }

        if (collision.gameObject.tag == "House 7" && Input.GetKey(KeyCode.E))
        {
            transform.position = new Vector3(18.84f, -0.04f, 13);
        }

        if (collision.gameObject.tag == "House 8" && Input.GetKey(KeyCode.E))
        {
            transform.position = new Vector3(22, 2.53f, 13);
        }

        if (collision.gameObject.tag == "House 9" && Input.GetKey(KeyCode.E))
        {
            transform.position = new Vector3(35.45f, 0.02f, 13);
        }

        if (collision.gameObject.tag == "House 10" && Input.GetKey(KeyCode.E))
        {
            transform.position = new Vector3(40.15f, 2.3f, 13);
        }

        //de här if-satserna kollar vilket dörr man är vid och teleporterar upp den till den exakta punkten. 

        #endregion

        #region Go Down

        if (collision.gameObject.tag == "House 1" && Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(-39.08f, -3.719342f, 13);
        }

        if (collision.gameObject.tag == "House 2" && Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(-31.77f, -3.719342f, 13);
        }

        if (collision.gameObject.tag == "House 3" && Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(-27.83f, -3.719342f, 13);
        }

        if (collision.gameObject.tag == "House 4" && Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(-17.82f, -3.719342f, 13);
        }

        if (collision.gameObject.tag == "House 5" && Input.GetKey(KeyCode.F))
        {
            print("go down");
            transform.position = new Vector3(-8.94f, -3.719342f, 13);
        }

        if (collision.gameObject.tag == "House 6" && Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(5.84f, -3.719342f, 13);
        }

        if (collision.gameObject.tag == "House 7" && Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(15.89f, -3.719342f, 13);
        }

        if (collision.gameObject.tag == "House 8" && Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(24.63f, -3.719342f, 13);
        }

        if (collision.gameObject.tag == "House 9" && Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(35.55f, -3.719342f, 13);
        }

        if (collision.gameObject.tag == "House 10" && Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(39.48f, -3.719342f, 13);
        }

        //Och de här if-satserna kollar vilket fönster/balkong man är vid/på och teleporterar tillbaka spelaren ner till dörren. 

        #endregion
    }
}
