using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimator{
	[Info (category = "Animator",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class Play : AnimatorAction {
		[FieldInfo(tooltip="The state hash")]
		public IntParameter hash;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			animator.Play (hash);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			animator.Play (hash);
		}
	}
}