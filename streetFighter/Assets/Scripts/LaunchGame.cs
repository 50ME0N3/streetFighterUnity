/* Project name : streetFighterUnity 
 * Date : 13.09.2021
 * Authors : Jordan, Grégoire, Antoine, Rémy, Gabriel
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchGame : StateMachineBehaviour
{
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// Si un joueur annule sa sélection le compte à rebours s'arrête
		if (animator.GetBool("Interrupt"))
		{
			animator.Play("Normal");
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// Si le compte à rebours arrive à zéro La partie commence
		if (!animator.GetBool("Interrupt"))
		{
			SceneManager.LoadScene("Battle");
		}
	}
}