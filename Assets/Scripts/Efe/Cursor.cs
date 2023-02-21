using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Camera maincam; // -Efe

    private Vector3 mousePos; // -Efe

    public Transform BulletTransform; // -Efe


    void Start() // -Efe
    {
        maincam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update() // g�r s� att vektor3 blir input.mouseposition och skapar en rotation vector p� z axis -Efe
    {
        mousePos = maincam.ScreenToWorldPoint(Input.mousePosition); // g�r s� att vektor3 blir input.mouseposition

        Vector3 rotation = mousePos - transform.position; // skapar en rotation vector p� z axis

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; 

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

   
}
