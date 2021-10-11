/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using System;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;

	// met la vie du joueur au max
	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

	// modifie la vie du joueur en fonction de la variable reçue
	public void takeDamage(int health)
	{
		slider.value -= health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

	public void heal(int health)
	{
		slider.value += health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

	public int getHealth()
	{
		return Convert.ToInt32(slider.value);
	}
}