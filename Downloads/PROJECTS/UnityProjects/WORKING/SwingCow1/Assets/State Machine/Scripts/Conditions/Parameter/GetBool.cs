using UnityEngine;
using System.Collections;


namespace StateMachine.Condition{
	[Info (category = "Parameter",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public class GetBool : StateCondition {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",requiredField=true, tooltip="Parameter to test.")]
		public BoolParameter parameter;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;
		
		public override bool Validate ()
		{
			return ((parameter.Value == true) == equals.Value);
		}
	}
}