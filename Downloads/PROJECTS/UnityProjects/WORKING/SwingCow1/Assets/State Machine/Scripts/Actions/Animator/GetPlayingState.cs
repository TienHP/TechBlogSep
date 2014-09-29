using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimator{
	[Info (category = "Animator",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class GetPlayingState : AnimatorAction {
		[FieldInfo(tooltip="Animator layer.")]
		public IntParameter layer;
		[FieldInfo(canBeConstant=false,nullLabel="None", tooltip="Store the name hash.")]
		public IntParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo (layer.Value);
			store.Value=info.nameHash;
			
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo (layer.Value);
			store.Value=info.nameHash;
		}
	}
}