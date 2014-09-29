﻿using UnityEngine;
using System.Collections;

namespace StateMachine.Condition.UnityAnimator{
	[Info (category = "Animator", 
	       description = "Is the specified AnimatorController layer in a transition.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Animator.IsInTransition.html")]
	[System.Serializable]
	public class IsInTransition : StateCondition {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner",tooltip="GameObject to test.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="The layer to test.")]
		public IntParameter layer;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;
		
		private Animator animator;
		
		public override void OnEnter ()
		{
			if (target.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter "+target.Name +" in condition "+GetType().ToString()+" is null. Condition disabled!");
				return;
			}
			
			animator = ((GameObject)target.Value).GetComponent<Animator> ();
			if (animator == null) {
				disabled=true;
				Debug.LogWarning("Missing Component! "+ GetType().ToString()+ " requires the Animator component on the GameObject. Condition disabled!");
			}
		}
		
		public override bool Validate ()
		{
			if(animator != null){

				return animator.IsInTransition(layer.Value) == equals.Value;
			}
			return false;
		}
	}
}