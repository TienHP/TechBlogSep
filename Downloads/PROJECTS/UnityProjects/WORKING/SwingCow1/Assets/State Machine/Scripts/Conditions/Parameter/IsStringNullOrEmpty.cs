using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Parameter",    
	       description = "Checks if the target string is null or empty.",
	       url = "")]
	[System.Serializable]
	public class IsStringNullOrEmpty : StateCondition {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Target string parameter to test.")]
		public StringParameter target;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;
		
		public override bool Validate ()
		{
			return string.IsNullOrEmpty(target.Value)== equals.Value;
		}
	}
}