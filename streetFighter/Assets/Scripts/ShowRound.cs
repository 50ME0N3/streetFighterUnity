using UnityEngine;
using UnityEngine.UI;

public class ShowRound : StateMachineBehaviour
{
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.gameObject.GetComponent<Text>().text = "Round " + animator.GetInteger("Round");
		animator.ResetTrigger("New Round");
	}
}