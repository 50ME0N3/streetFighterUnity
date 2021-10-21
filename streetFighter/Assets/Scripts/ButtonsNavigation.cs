/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Remy
 * Description : Gère la sélection des boutons
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsNavigation : MonoBehaviour
{
	/// <summary>
	/// Objet contrôlant les évènement de l'interface utilisateur
	/// </summary>
	EventSystem eventSystem;

	void Start()
	{
		// Initialisation
		eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
	}

	void Update()
	{
		// Si aucun bouton n'est sélectionné le premier de la liste est sélectionnées
		if (eventSystem.currentSelectedGameObject == null && GetComponentsInChildren<Button>(false).Length > 0)
		{
			GetComponentsInChildren<Button>(false)[0].Select();
		}
	}
}