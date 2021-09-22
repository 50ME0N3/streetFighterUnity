/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy
 */

using UnityEngine;

public class DoDamage : MonoBehaviour
{
    [SerializeField] byte damage = 10;
    [SerializeField] Vector2 knockbackLeft = new Vector2(10, 20);
    [SerializeField] Vector2 knockbackRight = new Vector2(-10, 20);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Transform player = collision.gameObject.transform;
            if (this.gameObject.transform.position.y > player.position.y)
            {
                collision.GetComponent<player>().healthbar.takeDamage(damage);
                collision.attachedRigidbody.velocity = knockbackLeft;
                Debug.Log("left");
            }
            else
            {
                collision.GetComponent<player>().healthbar.takeDamage(damage);
                collision.attachedRigidbody.velocity = knockbackRight;
                Debug.Log("Right");
            }
        }
    }
}