/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Gr�goire, Antoine, R�my, Gabriel
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
	/// <summary>
	/// Les noms des personnages s�lectionn�s
	/// </summary>
	public static string[] chosenCharactersNames;

	/// <summary>
	/// Personnages jouables
	/// </summary>
	GameObject[] characters;

	/// <summary>
	/// Couleur de base des bordure des personnages
	/// </summary>
	Color32 defaultColor;

	void Start()
	{
		// Initialisation des variables
		characters = GameObject.FindGameObjectsWithTag("Character");

		defaultColor = characters[0].GetComponent<Outline>().effectColor;

		Selection.Selections = new List<Selection>();
		new Selection(1, new Color32(255, 64, 64, Selection.GetAlpha(SelectionState.NotSelected))); // Joueur 1 - Rouge
		new Selection(2, new Color32(64, 64, 255, Selection.GetAlpha(SelectionState.NotSelected))); // Joueur 2 - Bleu
		chosenCharactersNames = new string[Selection.Selections.Count];

		Selection.Countdown = gameObject.GetComponent<Animator>();
	}

	void Update()
	{
		// Remise � z�ro des couleurs
		foreach (GameObject character in characters)
		{
			character.GetComponent<Outline>().effectColor = defaultColor;
		}

		// Appel des fonctions des curseurs
		foreach (Selection selection in Selection.Selections)
		{
			selection.CheckInputs();
			selection.Move(characters.Length - 1);
			selection.Validate();
			Selection.BlendColors();
		}

		// Mise � jour de l'interface
		if (Selection.Selections[0].CharacterIndex == Selection.Selections[1].CharacterIndex)
		{
			characters[Selection.Selections[0].CharacterIndex].GetComponent<Outline>().effectColor = Selection.BlendedColor;
		}
		else
		{
			characters[Selection.Selections[0].CharacterIndex].GetComponent<Outline>().effectColor = Selection.Selections[0].Color;
			characters[Selection.Selections[1].CharacterIndex].GetComponent<Outline>().effectColor = Selection.Selections[1].Color;
		}
	}
}

/// <summary>
/// S�lection du personnage
/// </summary>
class Selection
{
	#region Champs statiques
	/// <summary>
	/// Tous les objects de cette classe
	/// </summary>
	public static List<Selection> Selections = new List<Selection>();

	/// <summary>
	/// Couleurs m�lang�es (Quand deux joueurs s�lectionnent le m�me personnage)
	/// </summary>
	public static Color32 BlendedColor = new Color32(160, 64, 160, GetAlpha(SelectionState.NotSelected)); // Violet

	/// <summary>
	/// Compte � rebours pour le d�but de la partie
	/// </summary>
	public static Animator Countdown;
	#endregion

	#region Champs priv�s

	/// <summary>
	/// Num�ro du joueur
	/// </summary>
	byte _number;

	/// <summary>
	/// Index du personnage s�lectionn�
	/// </summary>
	byte _characterIndex;

	/// <summary>
	/// Si le joueur a appuy� sur la touche gauche
	/// </summary>
	public bool _pressedLeft = false;

	/// <summary>
	/// Si le joueur a appuy� sur la touche droite
	/// </summary>
	public bool _pressedRight = false;

	/// <summary>
	/// Si le joueur a appuy� sur la touche de validation
	/// </summary>
	public bool _pressedValidate = false;

	/// <summary>
	/// Si le joueur a valid� son choix
	/// </summary>
	bool _validated = false;

	/// <summary>
	/// Direction vers laquelle le joueur d�place son curseur (n�gatif = gauche, positif = droite)
	/// </summary>
	float _direction;

	/// <summary>
	/// Appui sur la touche de validation
	/// </summary>
	float _select;

	/// <summary>
	/// Couleur du curseur du joueur
	/// </summary>
	Color32 _color;
	#endregion

	#region Propri�t�s

	/// <summary>
	/// Index du personnage s�lectionn�
	/// </summary>
	public byte CharacterIndex { get => _characterIndex; set => _characterIndex = value; }

	/// <summary>
	/// Couleur du curseur du joueur
	/// </summary>
	public Color32 Color { get => _color; set => _color = value; }
	#endregion

	/// <summary>
	/// Curseur de s�lection du personnage
	/// </summary>
	/// <param name="number">Num�ro du joueur</param>
	/// <param name="color">Couleur du curseur</param>
	public Selection(byte number, Color color)
	{
		_number = number;
		CharacterIndex = (byte)(number - 1);
		Color = color;

		Selections.Add(this);
	}

	#region M�thodes statiques

	/// <summary>
	/// R�cup�re le niveau de transparence pour l'�tat de s�lection
	/// </summary>
	/// <param name="state">�tat de s�lection</param>
	/// <returns>Niveau de transparence (0 = totalement transparent, 255 = totalement opaque)</returns>
	public static byte GetAlpha(SelectionState state)
	{
		return (byte)(255 * ((float)state / 100));
	}

	/// <summary>
	/// M�lange les couleurs et la met � jour sur l'interface
	/// </summary>
	public static void BlendColors()
	{
		if (Selections[0].Color.a == GetAlpha(SelectionState.Selected) && Selections[1].Color.a == GetAlpha(SelectionState.Selected))
		{
			BlendedColor.a = GetAlpha(SelectionState.Selected);
		}
		else if (Selections[0].Color.a == GetAlpha(SelectionState.NotSelected) && Selections[1].Color.a == GetAlpha(SelectionState.NotSelected))
		{
			BlendedColor.a = GetAlpha(SelectionState.NotSelected);
		}
		else
		{
			BlendedColor.a = GetAlpha(SelectionState.HalfSelected);
		}
	}
	#endregion

	#region M�thodes publiques

	/// <summary>
	/// R�cup�re les touches sur lesquelles le joueur � appuy�
	/// </summary>
	public void CheckInputs()
	{
		_direction = Input.GetAxis("HorizontalPlayer" + _number);
		_select = Input.GetAxis("AttackPlayer" + _number);
	}

	/// <summary>
	/// D�place le curseur dans la direction choisie
	/// </summary>
	/// <param name="nbCharacters">Nombre de personnages disponibles</param>
	public void Move(int nbCharacters)
	{
		if (!_validated)
		{
			if (_direction != 0)
			{
				if (!_pressedLeft && !_pressedRight)
				{
					if (_direction > 0 && CharacterIndex < nbCharacters)
					{
						_pressedLeft = false;
						_pressedRight = true;

						CharacterIndex++;
					}
					else if (_direction < 0 && CharacterIndex > 0)
					{
						_pressedLeft = true;
						_pressedRight = false;

						CharacterIndex--;
					}
				}
			}
			else
			{
				_pressedLeft = false;
				_pressedRight = false;
			}
		}
	}

	/// <summary>
	/// Valide la s�lection du personnage
	/// </summary>
	/// <remarks>
	/// Lorsque tous les joueurs ont choisi leur personnage le compte � rebours se met en marche pour lancer le combat
	/// </remarks>
	public void Validate()
	{
		if (_select > 0)
		{
			if (!_pressedValidate)
			{
				_pressedValidate = true;

				if (Color.a == GetAlpha(SelectionState.NotSelected))
				{
					_color.a = GetAlpha(SelectionState.Selected);
					_validated = true;
					CharacterSelection.chosenCharactersNames[_number - 1] = GameObject.FindGameObjectsWithTag("Character")[_characterIndex].name;

					bool allSelected = true;

					foreach (Selection selection in Selections)
					{
						if (!selection._validated)
						{
							allSelected = false;
						}
					}

					if (allSelected)
					{
						Countdown.ResetTrigger("Interrupt");
						Countdown.SetTrigger("Selected");
					}
				}
				else
				{
					_color.a = GetAlpha(SelectionState.NotSelected);
					_validated = false;

					Countdown.ResetTrigger("Selected");
					Countdown.SetTrigger("Interrupt");
				}
			}
		}
		else
		{
			_pressedValidate = false;
		}
	}
	#endregion
}

/// <summary>
/// Si les joueurs ont valid� leurs s�lections
/// </summary>
public enum SelectionState : byte
{
	/// <summary>
	/// Pas valid�s
	/// </summary>
	NotSelected = 40,

	/// <summary>
	/// Un seul joueur a valid�
	/// </summary>
	HalfSelected = 70,

	/// <summary>
	/// Valid�s
	/// </summary>
	Selected = 100
}