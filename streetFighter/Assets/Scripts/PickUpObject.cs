/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Gabriel
 * Description : Fonctionnement de la pièce, détecte qui l'a touchée, déclenche et applique les différents pouvoirs, gère les animations de la pièce et des power ups.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
using System;

public class PickUpObject : MonoBehaviour
{
    Animator myAnimation;
    Animator animationTexte;

    public GameObject textePowerUp;
    public GameObject coin;
    public GameObject damagePlayer1;
    public GameObject damagePlayer2;
    public GameObject speedPlayer1;
    public GameObject speedPlayer2;

    randomSpawner randomSpawner;
    player player;
    KenKick scriptKenKick;
    KenPunch scriptKenPunch;
    ChunLiKick scriptLiKick;
    ChunLiPunch scriptLiPunch;

    public float multiplier = 2f;
    bool chunLi = false;
    int applyEffect;
    private float timeWait = 1.5f;
    private float timeWaitPowerUp = 8f;
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
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponentInChildren<player>();

            chunLi = CharactersSelection.chosenCharactersNames[int.Parse(collision.gameObject.name[collision.gameObject.name.Length - 1].ToString()) - 1] == "Chun-Li";


            if (!chunLi)
            {
                scriptKenKick = collision.gameObject.GetComponentInChildren<KenKick>(true);
                scriptKenPunch = collision.gameObject.GetComponentInChildren<KenPunch>(true);
            }
            else
            {
                scriptLiKick = collision.gameObject.GetComponentInChildren<ChunLiKick>(true);
                scriptLiPunch = collision.gameObject.GetComponentInChildren<ChunLiPunch>(true);
            }
            
            

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
        applyEffect = UnityEngine.Random.Range(0, 3);
        
        if (applyEffect == 0)
        {
            timeWait = 1.5f;
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
            Invoke("stopPowerUp", timeWaitPowerUp);
        }
        else
        {
            if(applyEffect == 1)
            {
                timeWait = 1.8f;
                myAnimation.SetBool("estToucherPower", true);
                // -Fait plus de dégats  
                if (player1)
                {
                    damagePlayer1.SetActive(true);
                }
                else
                {
                    damagePlayer2.SetActive(true);
                }

                if (chunLi)
                {
                    scriptLiKick.damageKick *= 2;
                    scriptLiPunch.damagePunch *= 2;
                }
                else
                {
                    scriptKenKick.damageKick *= 2;
                    scriptKenPunch.damagePunch *=2;
                }

                textePowerUp.SetActive(true);
                animationTexte.SetBool("animDamage", true);
                Invoke("stopAnim", timeWait);
                Invoke("stopPowerUp", timeWaitPowerUp);
            }
            else
            {
                timeWait = 1.9f;
                if(applyEffect == 2)     
                {
                    myAnimation.SetBool("estToucherVitesse", true);
                    // -Cours plus vite 
                    //player.speed = 10f;

                    if (player1)
                    {
                        speedPlayer1.SetActive(true);
                    }
                    else
                    {
                        speedPlayer2.SetActive(true);
                    }

                    player.speed *= 6;


                    textePowerUp.SetActive(true);
                    animationTexte.SetBool("animSpeed", true);
                    Invoke("stopAnim", timeWait);
                    Invoke("stopPowerUp", timeWaitPowerUp);
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
        animationTexte.SetBool("animDamage", false);
        animationTexte.SetBool("animHeal", false);
        animationTexte.SetBool("animSpeed", false);
    }

    private void stopPowerUp()
    {
        if(applyEffect == 1 && chunLi == true)
        {
            scriptLiKick.damageKick /= 2;
            scriptLiPunch.damagePunch /= 2;
        }
        else if(applyEffect == 1 && chunLi == false)
        {
            scriptKenKick.damageKick /= 2;
            scriptKenPunch.damagePunch /= 2;
        }
        else if(applyEffect == 2)
        {
            player.speed = 0.5f;
        }

        damagePlayer1.SetActive(false);
        damagePlayer2.SetActive(false);
        speedPlayer1.SetActive(false);
        speedPlayer2.SetActive(false);
        
        
    }
}