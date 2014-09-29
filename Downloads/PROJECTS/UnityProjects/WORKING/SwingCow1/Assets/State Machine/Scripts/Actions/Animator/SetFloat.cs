using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimator{
	[Info (category = "Animator",    
	       description = "Sets the value of a float parameter.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Animator.SetFloat.html")]
	[System.Serializable]
	public class SetFloat : AnimatorAction {
		[FieldInfo(tooltip="The animator float parameter name to set.")]
		public StringParameter parameter;
		[FieldInfo(tooltip="The time allowed to parameter to reach the value.")]
		public FloatParameter dampTime;
		[FieldInfo(tooltip="The value to set.")]
		public FloatParameter value;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			if (dampTime.Value > 0) {
				animator.SetFloat (parameter.Value, value.Value,dampTime.Value,Time.deltaTime);
			}else{
				animator.SetFloat (parameter.Value, value.Value);
			}
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			if (dampTime.Value > 0) {
				animator.SetFloat (parameter.Value, value.Value,dampTime.Value,Time.deltaTime);
			}else{
				animator.SetFloat (parameter.Value, value.Value);
			}
		}
	}
}