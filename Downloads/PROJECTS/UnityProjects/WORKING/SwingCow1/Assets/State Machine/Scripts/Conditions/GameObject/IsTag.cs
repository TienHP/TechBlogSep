using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "GameObject",    
	       description = "Checks if the game object's tag is equal to the test tag.",
	       url = "")]
	[System.Serializable]
	public class IsTag : StateCondition {
		[FieldInfo(canBeConstant=false,nullLabel="None",tooltip="GameObject to test.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="The tag to test with.")]
		public StringParameter tag;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;

		public override void OnEnter ()
		{
			if (target.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter "+target.Name +" in condition "+GetType().ToString()+" is null. Condition disabled!");
				return;
			}
		}

		public override bool Validate ()
		{
			return (((GameObject)target.Value).tag == tag.Value)== equals.Value;
		}
	}
}