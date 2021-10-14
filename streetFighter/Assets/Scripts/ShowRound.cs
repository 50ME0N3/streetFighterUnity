using UnityEngine;
using UnityEngine.UI;

public class ShowRound : StateMachineBehaviour
{
	/// <summary>
	/// Nombre de rounds maximum
	/// </summary>
	public const byte MAX_ROUND = 3;

	/// <summary>
	/// Numéro du round
	/// </summary>
	public static byte round = 1;

	/// <summary>
	/// Nombre de victoires des joueurs
	/// </summary>
	public static int[] score = new int[2] {0, 0};

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// Affichage du score
		for (int i = 0; i < score.Length; i++)
		{
			GameObject.Find("ScorePlayer" + (i + 1)).GetComponent<Text>().text = score[i].ToString();
		}

		// Affichage du round
		animator.gameObject.GetComponent<Text>().text = "Round " + round;

		animator.ResetTrigger("New Round");
	}
}