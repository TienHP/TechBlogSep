using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Parameter",    
	       description = "Is the first parameter equal to the second.",
	       url = "")]
	[System.Serializable]
	public class StringStartWith : StateCondition {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Target string parameter to test.")]
		public StringParameter target;
		[FieldInfo(tooltip="Start string sequence to test with.")]
		public StringParameter startWith;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;

		public override bool Validate ()
		{
			return target.Value.StartsWith (startWith.Value)== equals.Value;
		}
	}
}