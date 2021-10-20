/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan
 * Description : Controle le zoom de la caméra
 */

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	/// <summary>
	/// Vitesse de déplacement de la caméra
	/// </summary>
	const float MOVE_SPEED = 3;

	/// <summary>
	/// Vitesse de zoom de la caméra
	/// </summary>
	const float ZOOM_SPEED = 3;

	/// <summary>
	/// Multiplicateur du zoom de la caméra
	/// </summary>
	const float ZOOM = 1;

	/// <summary>
	/// Taille minimale de la caméra
	/// </summary>
	const float MIN_SIZE = 0.75f;

	/// <summary>
	/// Taille maximale de la caméra
	/// </summary>
	const float MAX_SIZE = 1.9f;

	/// <summary>
	/// Position z de la caméra
	/// </summary>
	float zPosition = 0;

	/// <summary>
	/// Position du joueur 1
	/// </summary>
	Transform transformPlayer1;

	/// <summary>
	/// Position du joueur 2
	/// </summary>
	Transform transformPlayer2;

	void Start()
	{
		// Initialisation des variables
		zPosition = gameObject.transform.position.z;

		transformPlayer1 = GameObject.Find("Player1").transform;
		transformPlayer2 = GameObject.Find("Player2").transform;

		// Ajustement du zoom de la caméra par rapport à la distance entre les joueurs
		GetComponent<Camera>().orthographicSize = Mathf.Clamp(Vector2.Distance(transformPlayer1.position, transformPlayer2.position) / ZOOM, MIN_SIZE, MAX_SIZE);
	}

	void Update()
	{
		// Mise en mémoire des positions des joueurs
		Vector2 positionPlayer1 = transformPlayer1.position;
		Vector2 positionPlayer2 = transformPlayer2.position;

		// Point équidistant entre les positions des joueurs
		Vector2 center = positionPlayer1 + (positionPlayer2 - positionPlayer1) / 2;

		// La caméra se déplace lentement vers le centre
		transform.position = Vector3.Lerp(transform.position, new Vector3(center.x, center.y, zPosition), Time.deltaTime * MOVE_SPEED);

		// Ajustement du zoom de la caméra par rapport à la distance entre les joueurs
		float targetSize = Mathf.Clamp(Vector2.Distance(positionPlayer1, positionPlayer2) / ZOOM, MIN_SIZE, MAX_SIZE);
		GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, targetSize, Time.deltaTime * ZOOM_SPEED);
	}
}