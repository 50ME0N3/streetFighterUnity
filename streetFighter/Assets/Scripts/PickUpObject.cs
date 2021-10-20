/* Project name : CFPT SMASH 
* Authors : Gabriel
*/+


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class PickUpObject : MonoBehaviour
{


    Animator myAnimation;
    Animator animationTexte;
    public GameObject textePowerUp;
    public GameObject coin;
    public float multiplier = 2f;
    randomSpawner randomSpawner;
    KenKick scriptKenKick;
    KenPunch scriptKenPunch;
    player player;
    private float timeWait = 1.5f;
    bool player1 = false;
    bool player2 = false;
    
    #region Global Variables
    #region Components

    /// <summary>
    /// Barre de vie du joueur
    /// </summary>
    public Healthbar healthBar;

    /// <summary>
    /// Objet qui détecte le contact avec le sol
    /// </summary>
    groundSensor groundSensor;

    /// <summary>
    /// Composant RigidBody du personnage
    /// </summary>
    Rigidbody2D rgbd;

    /// <summary>
    /// Arbre des animations du personnage
    /// </summary>
    Animator anim;
    #endregion

    #region Stats

    /// <summary>
    /// Nombre de points de vie du joueur
    /// </summary>
    int health = 100;

    /// <summary>
    /// Vitesse maximale de course atteignable
    /// </summary>
    const float MAX_SPEED = 2.5f;

    /// <summary>
    /// Vitesse de course du personnage
    /// </summary>
    float speed = 1f;

    /// <summary>
    /// Hauteur de saut du personnage
    /// </summary>
    float jumpHeight = 11;

    /// <summary>
    /// Vitesse de chute rapide
    /// </summary>
    float fastFallSpeed = 1.3f;

    /// <summary>
    /// Recul des attaques subies
    /// </summary>
    public Vector2 knockback = Vector2.zero;
    #endregion

    #region Cheats Enabled

    /// <summary>
    /// Si la régénération infinie est activée
    /// </summary>
    bool infiniteRegen = false;

    /// <summary>
    /// Si la mort instantanée est activée
    /// </summary>
    public bool instantDeath = false;

    /// <summary>
    /// Si le vol illimité est activé
    /// </summary>
    bool illimitedFly = false;
    #endregion

    #region Cheat Keys

    /// <summary>
    /// Touche activant la régénération infinie
    /// </summary>
    const KeyCode INFINITE_REGEN_KEY = KeyCode.Alpha1;

    /// <summary>
    /// Touche activant la mort instantanée
    /// </summary>
    const KeyCode INSTANT_DEATH_KEY = KeyCode.Alpha2;

    /// <summary>
    /// Touche activant le vol illimité
    /// </summary>
    const KeyCode ILLIMITED_FLY_KEY = KeyCode.Alpha3;
    #endregion

    #region Axis

    /// <summary>
    /// Appui sur la touche de saut
    /// </summary>
    string jumpAxis;

    /// <summary>
    /// Appui sur la touche de déplacement vers la gauche ou la droite
    /// </summary>
    string horizontalAxis;

    /// <summary>
    /// Appui sur la touche d'attaque 1
    /// </summary>
    string attack1Axis;

    /// <summary>
    /// Appui sur la touche de chute rapide
    /// </summary>
    string fastFallAxis;
    #endregion

    #region Booleans

    /// <summary>
    /// Si le joueur courait
    /// </summary>
    bool wasMoving = false;

    /// <summary>
    /// Si le joueur est mort
    /// </summary>
    bool dead = false;
    #endregion

    /// <summary>
    /// Moment où le joueur est mort
    /// </summary>
    float deathTime;

    /// <summary>
    /// Si le jeu est fini et que le menu de fin s'est affiché
    /// </summary>
    bool end = false;
    #endregion
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Récupère quel joueur touche la pièce
        if (collision.name == "Player1")
        {
            player1 = true;
            player2 = false;
        }
        else
        {
            if (collision.name == "Player2")
            {
                player2 = true;
                player1 = false;
            }
        }

        // Quand le joueur touche la pi�ce 
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            PickUp(collision);

            StartCoroutine(CoinDestroy());
            // attend 0.7 seconde pour detruire la pi�ce
            IEnumerator CoinDestroy()
            {
                yield return new WaitForSeconds(0.4f);
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                gameObject.SetActive(false);
            }
        }
    }

    void PickUp(Collider2D Player)
    {
        // Apply effect to the player :
        int applyEffect = Random.Range(0, 3);

        Debug.Log(applyEffect);
        if (applyEffect == 0)
        {
            // Fait l'animation
            myAnimation.SetBool("estToucher", true);

            // -Rajoute de la vie selon le joueur qui a toucher la pièce 

            if (player1)
            {
                healthBar = GameObject.FindGameObjectWithTag("SliderP1").GetComponent<Healthbar>();
                healthBar.Heal(50);

                player1 = false;
                player2 = false;
            }
            else if (player2)
            {
                healthBar = GameObject.FindGameObjectWithTag("SliderP2").GetComponent<Healthbar>();
                healthBar.Heal(50);

                player1 = false;
                player2 = false;
            }
            textePowerUp.SetActive(true);
            animationTexte.SetBool("animHeal", true);
            Invoke("stopAnim", timeWait);
        }
        else
        {
            if(applyEffect == 1)
            {
                myAnimation.SetBool("estToucherPower", true);
                // -Fait plus de dégats  
                //scriptKenKick.damageKick = 30;
                //scriptKenPunch.damagePunch = 25;
            }
            else
            {
                if(applyEffect == 2)     
                {
                    myAnimation.SetBool("estToucherVitesse", true);
                    // -Cours plus vite 
                    //player.speed = 10f;
                }
            }
        }
    }
    private void Start()
    {
        myAnimation = coin.GetComponent<Animator>(); 
        animationTexte = textePowerUp.GetComponent<Animator>();

    } 
    private void stopAnim()
    {
        textePowerUp.SetActive(false);
    }
}