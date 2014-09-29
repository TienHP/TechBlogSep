using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimator{
	[Info (category = "Animator",    
	       description = "Sets the look at position.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Animator.SetLookAtPosition.html")]
	[System.Serializable]
	public class SetLookAtPosition : AnimatorAction {
		[FieldInfo(tooltip="The position to lookAt.")]
		public Vector3Parameter position;
		
		public override void OnAnimatorIK (int layer)
		{
			animator.SetLookAtPosition (position.Value);
		}
	}
}