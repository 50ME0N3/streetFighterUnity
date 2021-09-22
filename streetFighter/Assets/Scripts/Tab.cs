using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
	Button[] buttons;

	bool[][] pressed =
	{
		new bool[]{ false, false }, // up
		new bool[]{ false, false } // Down
	};

	int index = 0;

	void Start()
	{
		buttons = GetComponentsInChildren<Button>();

		buttons[index].Select();
	}

	void Update()
	{
		float[] inputs = new float[2];

		for (int i = 0; i < inputs.Length; i++)
		{
			if (Input.GetAxis("JumpPlayer" + (i + 1)) > 0)
			{
				inputs[i] = Input.GetAxis("JumpPlayer" + (i + 1));
			}
			else if (Input.GetAxis("FastFallPlayer" + (i + 1)) > 0)
			{
				inputs[i] = -Input.GetAxis("FastFallPlayer" + (i + 1));
			}
			else
			{
				inputs[i] = 0;
			}

			if (inputs[i] != 0)
			{
				if (!pressed[i][0] && !pressed[i][1])
				{
					if (inputs[i] > 0 && index < buttons.Length)
					{
						pressed[i][0] = true;
						pressed[i][1] = false;
						index--;
					}
					else if (inputs[i] < 0 && index > 0)
					{
						pressed[i][0] = false;
						pressed[i][0] = true;

						index++;
					}
				}
			}
			else
			{
				pressed[i][0] = false;
				pressed[i][1] = false;
			}

			if (index < 0)
			{
				index = 0;
			}
			else if (index >= buttons.Length)
			{
				index = buttons.Length - 1;
			}
		}

		buttons[index].Select();
	}
}