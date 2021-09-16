using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //TODO: ajout du kb
            collision.GetComponent<player>().healthbar.takeDamage(10);
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 20);
        }
    }
}
