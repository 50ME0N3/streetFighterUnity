/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Gr�goire, Antoine, R�my, Gabriel
 */

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	const float MULTIPLIER = 3.5f;
	const int MIN = 1;
	const int MAX = 2;

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

		if (distance / MULTIPLIER <= MIN)
		{
			GetComponent<Camera>().orthographicSize = MIN;
		}
		else if (distance / MULTIPLIER >= MAX)
		{
			GetComponent<Camera>().orthographicSize = MAX;
		}
		else
		{
			GetComponent<Camera>().orthographicSize = distance / MULTIPLIER;
		}

		Vector2 center = positionPlayer1 + (positionPlayer2 - positionPlayer1) / 2;
		bool touchedBorder;
		int antiInfinite = 0;

		do
		{
			antiInfinite++;
			touchedBorder = false;

			foreach (RaycastHit2D hit in Physics2D.BoxCastAll(center, new Vector2(2 * GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect, 2 * GetComponent<Camera>().orthographicSize), 0, Vector2.zero))
			{
				switch (hit.collider.name)
				{
					case "mur gauche":
						center = new Vector2(center.x + 0.1f, center.y);
						touchedBorder = true;
						break;
					case "mur droite":
						center = new Vector2(center.x - 0.1f, center.y);
						touchedBorder = true;
						break;
					case "toit":
						center = new Vector2(center.x, center.y - 0.1f);
						touchedBorder = true;
						break;
				}
			}
		}
		while (touchedBorder && antiInfinite < 100);

		transform.position = new Vector3(center.x, center.y, zPosition);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, new Vector2(2 * GetComponent<Camera>().orthographicSize * GetComponent<Camera>().aspect, 2 * GetComponent<Camera>().orthographicSize));
	}
}