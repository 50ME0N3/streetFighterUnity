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
    // Initialisation des variables 
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
        // Traite le code que si c'est un joueur qui touche la pièce 
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponentInChildren<player>();

            // Récupère si le personnage avec le quelle le joueur touche la pièce est Chun Li ou Ken 
            chunLi = CharactersSelection.chosenCharactersNames[int.Parse(collision.gameObject.name[collision.gameObject.name.Length - 1].ToString()) - 1] == "Chun-Li";


            // Applique le script au bon personnage  
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
            else if (collision.name == "Player2")
                {
                    player2 = true;
                    player1 = false;
                }
            

                // Quand le joueur touche la pièce les colision de la pièce sont désactiver pour eviter que le joueur touche deux fois la pièce 
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                PickUp(collision);

                StartCoroutine(CoinDestroy());
                // attend 0.4 seconde pour désactiver la pi�ce et réactiver la collision de la pièce 
                IEnumerator CoinDestroy()
                {
                    yield return new WaitForSeconds(0.4f);
                    gameObject.GetComponent<CircleCollider2D>().enabled = true;
                    gameObject.SetActive(false);
                }
            
        }
        
    }

    #region Méthodes crées
    /// <summary>
    /// Méthodes qui applique les pouvoirs aléatoirement au joueur
    /// </summary>
	/// <param name="Player"></param>
    void PickUp(Collider2D Player)
    {
        // Nombre aléatoire pour prendre aléatoirement les pouvoirs et les appliqué
        applyEffect = UnityEngine.Random.Range(0, 3);
        
        if (applyEffect == 0)// Rajoute 50 pv de vie
        {
            timeWait = 1.5f;
            // Fait l'animation
            myAnimation.SetBool("estToucher", true);

            // -Rajoute de la vie selon le joueur qui a toucher la pièce 
            if (player1)
            {
                healPlayer("SliderP1");

                player1 = false;
                player2 = false;
            }
            else if (player2)
            {
                healPlayer("SliderP2");

                player1 = false;
                player2 = false;
            }

            // Joue les animation 
            makeAnim("animHeal");
            // Arrête les animations et les pouvoirs
            stopAnimAndPower(timeWait, timeWaitPowerUp);
        }
        else
        {
            if(applyEffect == 1) // -Fait plus de dégats 
            {
                timeWait = 1.8f;
                myAnimation.SetBool("estToucherPower", true);
                 
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
                    scriptKenPunch.damagePunch *= 2;
                }
                // Joue les animation 
                makeAnim("animDamage");
                // Arrête les animations et les pouvoirs
                stopAnimAndPower(timeWait, timeWaitPowerUp);
            }
            else
            {
                
                if(applyEffect == 2)// -Cours plus vite 
                {
                    timeWait = 1.9f;
                    myAnimation.SetBool("estToucherVitesse", true);
                    
                    if (player1)
                    {
                        speedPlayer1.SetActive(true);
                    }
                    else
                    {
                        speedPlayer2.SetActive(true);
                    }

                    player.speed *= 6;

                    // Joue les animation 
                    makeAnim("animSpeed");
                    // Arrête les animations et les pouvoirs
                    stopAnimAndPower(timeWait,timeWaitPowerUp);
                }
            }
        }
    }

    

    /// <summary>
    /// Appel les méthode d'arret d'animation et de pouvoirs après un certains temp (deuxième paramètre entre parenthèse)
    /// </summary>
    private void stopAnimAndPower(float timeWait, float timeWaitPowerUp)
    {
        Invoke("stopAnim", timeWait);
        Invoke("stopPowerUp", timeWaitPowerUp);
    }
    /// <summary>
    /// Active l'objet qui affiche l'animation et joue l'animation 
    /// </summary>
    private void makeAnim(string nameAnim)
    {
        textePowerUp.SetActive(true);
        animationTexte.SetBool(nameAnim, true);
    }
    /// <summary>
    /// Prend la healthBar su joueur qui a toucher la pièce et appel la fonction qui rajoute 50 pv( chaque joueur on 100pv au maximum)
    /// </summary>
    private void healPlayer(string slider)
    {
        healthBar = GameObject.FindGameObjectWithTag(slider).GetComponent<Healthbar>();
        healthBar.Heal(50);
    }

    /// <summary>
    /// Méthodes au start
    /// </summary>
    private void Start()
    {
        myAnimation = coin.GetComponent<Animator>(); 
        animationTexte = textePowerUp.GetComponent<Animator>();
    }

    /// <summary>
    /// Arrête toute les animation texte active 
    /// </summary>
    private void stopAnim()
    {
        animationTexte.SetBool("animDamage", false);
        animationTexte.SetBool("animHeal", false);
        animationTexte.SetBool("animSpeed", false);
    }

    /// <summary>
    /// Stop tout les power up en les remettant a la valeur initial
    /// </summary>
    private void stopPowerUp()
    {
        // Remet les valeur de dégat et de vitesse a leur valeur initial
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
        // Désactive en dessous de l'image du joueur les power up 
        damagePlayer1.SetActive(false);
        damagePlayer2.SetActive(false);
        speedPlayer1.SetActive(false);
        speedPlayer2.SetActive(false);
    }
    #endregion
}