/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy
 */

using UnityEngine;

public class DoDamage : MonoBehaviour
{
    [SerializeField] byte damage = 10;
    [SerializeField] Vector2 knockback = new Vector2(10, 20);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<player>().healthbar.takeDamage(damage);
            collision.attachedRigidbody.velocity = knockback;
        }
    }
}