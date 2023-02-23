using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public bool usingPistol; //för att kolla vilken animation som ska spelas när man skjuter :) -Simon
    public bool usingCrossbow; //för att kolla vilken animation som ska spelas när man skjuter :) -Simon

    public Transform firepoint; // -Efe

    public Transform rotatePoint; // -Efe

    public int damage = 40; // -Efe

    public bool canFire; // -Efe

    private float timer; // -Efe

    public float TimebetweeenFiring; // -Efe

    public LayerMask mask; // -Efe

    public GameObject bullet; // -Efe

    Player player; //Simon

    private void Start() //Simon
    {
        player = FindObjectOfType<Player>(); //hämtar spelar koden i scenen (finns bara 1)
    }

    private void Update() // om man trycker mouse 0, aktiveras "startcoroutine(shoot)" -Efe
    {
        if (Input.GetMouseButtonDown(0) && canFire) 
        {
            StartCoroutine(Shoot());

            canFire = false;
        }

      else  if (Input.GetMouseButton(0) && canFire) // om man aldrig släpper aktiveras "hold to shoot"
        {
            StartCoroutine(Shoot());

            canFire = false;

            if(TimebetweeenFiring <=0)
            {
                StartCoroutine(Shoot());
            }
        }

        if (!canFire) // -Efe // ger tid mellan varje gång man skjuter 
        {
            timer += Time.deltaTime;

            if (timer > TimebetweeenFiring) // -Efe
            {
                canFire = true;

                timer = 0;
            }
        }
    }


    IEnumerator Shoot() 
    {
        Vector2 dir = -(rotatePoint.position - firepoint.position).normalized; // får skotten spawn'a i direktionen av "firepoint" -Efe

        RaycastHit2D hitinfo = Physics2D.Raycast(firepoint.position, dir, 60f, mask); // lägger en osynlig raycast i positionen av "firepoint" -Efe 

        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation); // ger direction till skotten("newBullet") som-     
 newBullet.transform.up = dir;  // -är direktionen av dir

        yield return new WaitForSeconds(0.02f); // -Efe

        if (usingPistol)
        {
            player.anim.SetTrigger("PistolShoot"); // Simon
        }

        else if (usingCrossbow)
        {
            player.anim.SetTrigger("CrossbowShoot"); // Simon
        }
    }
   
}
