using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimator{
	[Info (category = "Animator",   
	       description = "Gets the value of a float parameter.",
	       url = "http://docs.unity3d.com/ScriptReference/Animator.GetFloat.html")]
	[System.Serializable]
	public class GetFloat : AnimatorAction {
		[FieldInfo(tooltip="The animator bool parameter name to set.")]
		public StringParameter parameter;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Store the result.")]
		public FloatParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			store.Value = animator.GetFloat (parameter.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = animator.GetFloat (parameter.Value);
		}
	}
}