using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pbullet : MonoBehaviour
{
        private void Update()
        {
            transform.position += transform.up * Time.deltaTime * 100;
        }
}
