using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
	GameObject[] characters;

	void Start()
	{
		characters = GameObject.FindGameObjectsWithTag("Character");

		new Selection(1, new Color32(255, 64, 64, 128)); // Red
		new Selection(2, new Color32(64, 64, 255, 128)); // Blue

		Selection.Countdown = gameObject.GetComponent<Animator>();
	}

	void Update()
	{
		foreach (GameObject character in characters)
		{
			character.GetComponent<Image>().color = Color.white;
		}

		foreach (Selection selection in Selection.Selections)
		{
			selection.CheckInputs();
			selection.Move(characters.Length - 1);
			selection.GetSelect();

			Selection.BlendColors();
		}

		if (Selection.Selections[0].CharacterIndex == Selection.Selections[1].CharacterIndex)
		{
			characters[Selection.Selections[0].CharacterIndex].GetComponent<Image>().color = Selection.BlendedColor;
		}
		else
		{
			characters[Selection.Selections[0].CharacterIndex].GetComponent<Image>().color = Selection.Selections[0].Color;
			characters[Selection.Selections[1].CharacterIndex].GetComponent<Image>().color = Selection.Selections[1].Color;
		}
	}
}

class Selection
{
	public static List<Selection> Selections = new List<Selection>();
	public static Color32 BlendedColor = new Color32(160, 64, 160, 128); // Purple
	public static Animator Countdown;

	byte _number;
	byte _characterIndex;
	bool _pressedLeft = false, _pressedRight = false, _pressedSelect = false;
	bool _selected = false;
	float _direction;
	float _select;

	Color32 _color;

	public byte CharacterIndex { get => _characterIndex; set => _characterIndex = value; }
	public Color32 Color { get => _color; set => _color = value; }

	public Selection(byte number, Color color)
	{
		_number = number;
		CharacterIndex = (byte)(number - 1);
		Color = color;

		Selections.Add(this);
	}

	public static void BlendColors()
	{
		if (Selections[0].Color.a == 255 && Selections[1].Color.a == 255)
		{
			BlendedColor.a = 255;
		}
		else if (Selections[0].Color.a == 128 && Selections[1].Color.a == 128)
		{
			BlendedColor.a = 128;
		}
		else
		{
			BlendedColor.a = 192;
		}
	}

	public void CheckInputs()
	{
		_direction = Input.GetAxis("HorizontalPlayer" + _number);
		_select = Input.GetAxis("AttackPlayer" + _number);
	}

	public void Move(int nbCharacters)
	{
		if (_direction > 0 && CharacterIndex < nbCharacters)
		{
			_pressedLeft = false;
			_pressedRight = true;
		}
		else if (_direction < 0 && CharacterIndex > 0)
		{
			_pressedLeft = true;
			_pressedRight = false;
		}
		else if (_direction == 0)
		{
			if (_pressedLeft)
			{
				CharacterIndex--;
				_pressedLeft = false;
			}
			else if (_pressedRight)
			{
				CharacterIndex++;
				_pressedRight = false;
			}
		}
	}

	public void GetSelect()
	{
		if (_select > 0)
		{
			_pressedSelect = true;
		}
		else if (_pressedSelect)
		{
			_pressedSelect = false;

			if (Color.a == 128)
			{
				_color.a = 255;
				_selected = true;

				bool allSelected = true;

				foreach (Selection selection in Selections)
				{
					if (!selection._selected)
					{
						allSelected = false;
					}
				}

				if (allSelected)
				{
					Countdown.SetBool("Interrupt", false);
					Countdown.SetTrigger("Selected");
				}
			}
			else
			{
				_color.a = 128;
				_selected = false;

				Countdown.ResetTrigger("Selected");
				Countdown.SetBool("Interrupt", true);
			}
		}
	}
}