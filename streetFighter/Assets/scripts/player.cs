/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy
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
        rgbd = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float jumpInput = Input.GetAxis("Jump");
        Debug.Log(jumpInput);
        Debug.Log(groundSensor.Grounded);
        if (groundSensor.Grounded && jumpInput>0)
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x, jumpForce);
        }
    }
}