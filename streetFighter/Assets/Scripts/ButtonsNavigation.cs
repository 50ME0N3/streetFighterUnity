/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Remy
 * Description : G�re la selection des boutons
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsNavigation : MonoBehaviour
{
	/// <summary>
	/// Objet contr�lant les �v�nement de l'interface utilisateur
	/// </summary>
	EventSystem eventSystem;

	void Start()
	{
		// Initialisation
		eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
	}

	void Update()
	{
		// Si aucun bouton n'est s�lectionn� le premier de la liste est s�lectionn�es
		if (eventSystem.currentSelectedGameObject == null && GetComponentsInChildren<Button>(false).Length > 0)
		{
			GetComponentsInChildren<Button>(false)[0].Select();
		}
	}
}