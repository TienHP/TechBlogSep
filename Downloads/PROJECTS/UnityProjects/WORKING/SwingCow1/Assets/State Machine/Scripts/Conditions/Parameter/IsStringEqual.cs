using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Parameter",    
	       description = "Is the first parameter equal to the second.",
	       url = "")]
	[System.Serializable]
	public class IsStringEqual : StateCondition {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="First parameter.")]
		public StringParameter first;
		[FieldInfo(tooltip="Second parameter.")]
		public StringParameter second;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;

		public override bool Validate ()
		{
			return (first.Value == second.Value)== equals.Value;
		}
	}
}