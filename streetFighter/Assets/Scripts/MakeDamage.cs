/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Antoine
 * Description : Détecte quand un joueur en attaque un autre et applique les dégâts
 */

using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    /// <summary>
    /// Donne un coup au joueur touché
    /// </summary>
    /// <param name="collider">Hit box de la cible</param>
    /// <param name="damage">Dégâts de l'attaque</param>
    /// <param name="knockback">Éjection</param>
    /// <param name="hitBoxObject">Coup</param>
    public static void Hit(Collider2D collider, byte damage, Vector2 knockback, GameObject hitBoxObject)
    {
        // Position du joueur
        Transform player = collider.gameObject.transform;

        Damage(collider, damage, knockback, hitBoxObject.gameObject, hitBoxObject.transform.parent.position.x < player.position.x);

        /// <summary>
        /// Lance le coup
        /// </summary>
        /// <param name="collider">Hit box de la cible</param>
        /// <param name="damage">Dégâts de l'attaque</param>
        /// <param name="knockback">Éjection</param>
        /// <param name="hitBoxObject">Coup</param>
        /// <param name="left">Si l'attaquant est à gauche de la cible</param>
        void Damage(Collider2D collider, byte damage, Vector2 knockback, GameObject hitBoxObject, bool left)
        {
            // Inflige les dégâts à la cible
            collider.GetComponent<player>().healthBar.TakeDamage(damage);

            // Donne du recul à la cible
            if (left)
            {
                collider.GetComponent<player>().knockback = knockback;
            }
            else
            {
                collider.GetComponent<player>().knockback = new Vector2(-knockback.x, knockback.y);
            }
        }
    }
}