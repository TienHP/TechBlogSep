using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Parameter",    
	       description = "Is the parameter null?",
	       url = "")]
	[System.Serializable]
	public class IsNull : StateCondition {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",requiredField=true, tooltip="Parameter to test.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;

		public override bool Validate ()
		{
			return ((target.Value == null) == equals.Value);
		}
	}
}