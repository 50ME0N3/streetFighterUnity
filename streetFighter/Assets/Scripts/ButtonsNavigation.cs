using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsNavigation : MonoBehaviour
{
	EventSystem eventSystem;

	void Start()
	{
		eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
	}

	void Update()
	{
		if (eventSystem.currentSelectedGameObject == null && GetComponentsInChildren<Button>(false).Length > 0)
		{
			GetComponentsInChildren<Button>(false)[0].Select();
		}
	}
}