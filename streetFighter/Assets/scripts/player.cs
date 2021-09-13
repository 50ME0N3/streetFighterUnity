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

    public float jumpForce = 1F;
    void Start()
    {
        Debug.Log("TKT FRERO");
    }

    // Update is called once per frame
    void Update()
    {
        if (!groundSensor.Grounded)
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x, jumpForce);
        }
    }
}