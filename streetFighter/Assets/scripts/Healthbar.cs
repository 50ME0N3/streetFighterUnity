/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Antoine, Gabriel
 * Description : Gère les barres de vie des joueurs
 */

using System;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	/// <summary>
	/// Composant de la jauge
	/// </summary>
	public Slider slider;

	/// <summary>
	/// Dégradé de la jauge
	/// </summary>
	public Gradient gradient;

	/// <summary>
	/// Jauge
	/// </summary>
	public Image fill;

	/// <summary>
	/// Retourne le nombre de points de vie
	/// </summary>
	/// <returns>Les PV du joueur</returns>
	public int Health => Convert.ToInt32(slider.value);

	/// <summary>
	/// Met les PV du joueur au max
	/// </summary>
	/// <param name="health">Points de vie max</param>
	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

	/// <summary>
	/// Modifie les PV du joueur en fonction de la variable reçue
	/// </summary>
	/// <param name="damage">Nombre de points de vie retirés</param>
	public void TakeDamage(int damage)
	{
		slider.value -= damage;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

	/// <summary>
	/// Soigne le joueur
	/// </summary>
	/// <param name="hp"></param>
	public void Heal(int hp)
	{
		slider.value += hp;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}