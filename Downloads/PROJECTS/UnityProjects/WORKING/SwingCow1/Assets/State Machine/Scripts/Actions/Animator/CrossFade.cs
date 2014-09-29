using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimator{
	[Info (category = "Animator",   
	       description = "Create a dynamic transition between the current state and the destination state.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Animator.CrossFade.html")]
	[System.Serializable]
	public class CrossFade : AnimatorAction {
		[FieldInfo(tooltip="Layer index containing the destination state.")]
		public IntParameter layer;
		[FieldInfo(tooltip="The name of the destination state.")]
		public StringParameter stateName;
		[FieldInfo(tooltip="The duration of the transition. Value is in source state normalized time.")]
		public FloatParameter transitionDuration;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			animator.CrossFade (stateName.Value, transitionDuration.Value, layer.Value);
			if (!everyFrame) {
				Finish();			
			}
		}

		public override void OnUpdate ()
		{
			animator.CrossFade (stateName.Value, transitionDuration.Value, layer.Value);
		}
	}
}