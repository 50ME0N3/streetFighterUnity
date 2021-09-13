/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Gr�goire, Antoine, R�my
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public groundSensor groundSensor;
    private Rigidbody2D rgbd;

    public float speed;
    public float jumpForce;
    void Start()
    {
        rgbd = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float jumpInput = Input.GetAxis("Jump" + this.name);
        float direction = Input.GetAxis("Horizontal" + this.name);
        if (groundSensor.Grounded && jumpInput > 0)
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x, jumpForce);
        }

        if (direction > 0)
        {
            rgbd.velocity = new Vector2(direction * speed, rgbd.velocity.y);
        }
        else if (direction < 0)
        {
            rgbd.velocity = new Vector2(direction * speed, rgbd.velocity.y);
        }
        else
        {
            rgbd.velocity = new Vector2(0,rgbd.velocity.y);
        }
    }
}