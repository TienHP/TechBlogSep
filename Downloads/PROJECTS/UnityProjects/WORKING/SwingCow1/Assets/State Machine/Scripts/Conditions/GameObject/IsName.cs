using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "GameObject", 
	       description = "Does the name of the game object equals a string value.",
	       url = "")]
	[System.Serializable]
	public class IsName : StateCondition {
		[FieldInfo(canBeConstant=false,nullLabel="None",tooltip="GameObject to test.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="The string to test with.")]
		public StringParameter testString;
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
			return (((GameObject)target.Value).name == testString.Value)== equals.Value;
		}
	}
}