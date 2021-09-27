/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Gr�goire, Antoine, R�my, Gabriel
 */

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float MULTIPLIER = 2.5f;
	public int MIN = 5;
	public int MAX = 9;

	float zPosition = 0;

	Transform transformPlayer1;
	Transform transformPlayer2;

	void Start()
	{
		zPosition = gameObject.transform.position.z;

		transformPlayer1 = GameObject.Find("Player1").transform;
		transformPlayer2 = GameObject.Find("Player2").transform;
	}

	void Update()
	{
		Vector2 positionPlayer1 = transformPlayer1.position;
		Vector2 positionPlayer2 = transformPlayer2.position;

		float distance = Vector2.Distance(positionPlayer1, positionPlayer2);

		Vector2 center = positionPlayer1 + (positionPlayer2 - positionPlayer1) / 2;

		gameObject.transform.position = new Vector3(center.x, center.y, zPosition);

		if (distance / MULTIPLIER <= MIN)
		{
			gameObject.GetComponent<Camera>().orthographicSize = MIN;
		}
		else if (distance / MULTIPLIER >= MAX)
		{
			gameObject.GetComponent<Camera>().orthographicSize = MAX;
		}
		else
		{
			gameObject.GetComponent<Camera>().orthographicSize = distance / MULTIPLIER;
		}
	}
}
