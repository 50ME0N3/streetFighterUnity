using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 0.1F;

    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        if (direction > 0)
		{
            rigidbody.velocity = Vector2.right * speed;
		}
        else if (direction < 0)
		{
            rigidbody.velocity = Vector2.left * speed;
		}
        else
		{
            rigidbody.velocity = Vector2.zero;
        }
    }
}