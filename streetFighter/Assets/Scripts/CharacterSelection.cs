using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
	GameObject[] characters;

	Color32 defaultColor;

	void Start()
	{
		characters = GameObject.FindGameObjectsWithTag("Character");

		defaultColor = characters[0].GetComponent<Outline>().effectColor;

		Selection.Selections = new List<Selection>();
		new Selection(1, new Color32(255, 64, 64, Selection.GetAlpha(SelectionState.NotSelected))); // Red
		new Selection(2, new Color32(64, 64, 255, Selection.GetAlpha(SelectionState.NotSelected))); // Blue

		Selection.Countdown = gameObject.GetComponent<Animator>();
	}

	void Update()
	{
		foreach (GameObject character in characters)
		{
			character.GetComponent<Outline>().effectColor = defaultColor;
		}

		foreach (Selection selection in Selection.Selections)
		{
			selection.CheckInputs();
			selection.Move(characters.Length - 1);
			selection.Select();
			Selection.BlendColors();
		}

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

class Selection
{
	public static List<Selection> Selections = new List<Selection>();
	public static Color32 BlendedColor = new Color32(160, 64, 160, GetAlpha(SelectionState.NotSelected)); // Purple
	public static Animator Countdown;

	byte _number;
	byte _characterIndex;
	public bool _pressedLeft = false, _pressedRight = false, _pressedSelect = false;
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

	public static byte GetAlpha(SelectionState state)
	{
		return (byte)(255 * ((float)state / 100));
	}

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

	public void CheckInputs()
	{
		_direction = Input.GetAxis("HorizontalPlayer" + _number);
		_select = Input.GetAxis("AttackPlayer" + _number);
	}

	public void Move(int nbCharacters)
	{
		if (!_selected)
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

	public void Select()
	{
		if (_select > 0)
		{
			if (!_pressedSelect)
			{
				_pressedSelect = true;

				if (Color.a == GetAlpha(SelectionState.NotSelected))
				{
					_color.a = GetAlpha(SelectionState.Selected);
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
						Countdown.ResetTrigger("Interrupt");
						Countdown.SetTrigger("Selected");
					}
				}
				else
				{
					_color.a = GetAlpha(SelectionState.NotSelected);
					_selected = false;

					Countdown.ResetTrigger("Selected");
					Countdown.SetTrigger("Interrupt");
				}
			}
		}
		else
		{
			_pressedSelect = false;
		}
	}
}

public enum SelectionState : byte
{
	NotSelected = 40,
	HalfSelected = 70,
	Selected = 100
}