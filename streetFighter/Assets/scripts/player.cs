/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Gr�goire, Antoine, R�my
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GroundSensor groundSensor;
    private Rigidbody2D rgbd;

    public float speed;
    public float jumpForce;

    void Start()
    {
        rgbd = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float jumpInput = Input.GetAxis("Jump" + name);
        float direction = Input.GetAxis("Horizontal" + name);

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