using UnityEngine;
using System.Collections;

namespace StateMachine.Condition.UnityAnimator{
	[Info (category = "Animator",  
	       description = "Gets the value of a float parameter.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Animator.GetFloat.html")]
	[System.Serializable]
	public class GetFloat : StateCondition {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner",tooltip="GameObject to test.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="The animator float parameter name to test.")]
		public StringParameter parameter;
		[FieldInfo(tooltip="Is the parameter greater or less?")]
		public FloatComparer comparer;
		[FieldInfo(tooltip="Value to test with.")]
		public FloatParameter value;

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
			float floatValue = animator.GetFloat (parameter.Value);
			switch (comparer) {
			case FloatComparer.Less:
				return floatValue < value.Value;
			case FloatComparer.Greater:
				return floatValue > value.Value;
			}
			return false;
		}
	}
}