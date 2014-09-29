using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimator{
	[Info (category = "Animator",  
	       description = "Sets the position of an IK goal.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Animator.SetIKPosition.html")]
	[System.Serializable]
	public class SetIKPosition : AnimatorAction {
		[FieldInfo(tooltip="The AvatarIKGoal that is set.")]
		public AvatarIKGoal goal;
		[FieldInfo(tooltip="The position in world space.")]
		public Vector3Parameter position;

		public override void OnAnimatorIK (int layer)
		{
			animator.SetIKPosition (goal, position.Value);
		}
	}
}