/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	const float SPEED = 3;

	public float ZOOM = 1;
	public float MIN = 0.75f;
	public float MAX = 1.9f;

	float zPosition = 0;

	Transform transformPlayer1;
	Transform transformPlayer2;

	void Start()
	{
		zPosition = gameObject.transform.position.z;

		transformPlayer1 = GameObject.Find("Player1").transform;
		transformPlayer2 = GameObject.Find("Player2").transform;

		Vector2 positionPlayer1 = transformPlayer1.position;
		Vector2 positionPlayer2 = transformPlayer2.position;

		float distance = Vector2.Distance(positionPlayer1, positionPlayer2);

		if (distance / ZOOM <= MIN)
		{
			GetComponent<Camera>().orthographicSize = MIN;
		}
		else if (distance / ZOOM >= MAX)
		{
			GetComponent<Camera>().orthographicSize = MAX;
		}
		else
		{
			GetComponent<Camera>().orthographicSize = distance / ZOOM;
		}
	}

	void Update()
	{
		Vector2 positionPlayer1 = transformPlayer1.position;
		Vector2 positionPlayer2 = transformPlayer2.position;

		Vector2 center = positionPlayer1 + (positionPlayer2 - positionPlayer1) / 2;

		transform.position = Vector3.Lerp(transform.position, new Vector3(center.x, center.y, zPosition), Time.deltaTime * SPEED);

		float distance = Vector2.Distance(positionPlayer1, positionPlayer2);

		if (distance / ZOOM <= MIN)
		{
			GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, MIN, Time.deltaTime * SPEED);
		}
		else if (distance / ZOOM >= MAX)
		{
			GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, MAX, Time.deltaTime * SPEED);
		}
		else
		{
			GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, distance / ZOOM, Time.deltaTime * SPEED);
		}
	}
}