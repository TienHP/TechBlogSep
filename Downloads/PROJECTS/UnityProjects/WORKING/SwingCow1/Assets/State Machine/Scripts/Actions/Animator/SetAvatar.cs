using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimator{
	[Info (category = "Animator",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public class SetAvatar : AnimatorAction {
		[FieldInfo(canBeConstant=false,nullLabel="None",tooltip="Avatar to use.")]
		public ObjectParameter avatar;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			animator.avatar = (Avatar)avatar.Value;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			animator.avatar = (Avatar)avatar.Value;
		}
	}
}