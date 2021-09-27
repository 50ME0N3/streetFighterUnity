using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchGame : StateMachineBehaviour
{
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (animator.GetBool("Interrupt"))
		{
			animator.Play("Normal");
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!animator.GetBool("Interrupt"))
		{
			Selection.Selections = new List<Selection>();
			SceneManager.LoadScene("Battle");
		}
	}
}