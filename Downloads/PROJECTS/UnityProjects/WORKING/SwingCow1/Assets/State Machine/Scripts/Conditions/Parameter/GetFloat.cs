using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Parameter",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public class GetFloat : StateCondition {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",requiredField=true, tooltip="Parameter to test.")]
		public FloatParameter parameter;
		[FieldInfo(tooltip="Is the parameter greater or less?")]
		public FloatComparer comparer;
		[FieldInfo(tooltip="Value to test with.")]
		public FloatParameter value;
		
		public override bool Validate ()
		{
			switch (comparer) {
			case FloatComparer.Less:
				return parameter.Value < value.Value;
			case FloatComparer.Greater:
				return parameter.Value > value.Value;
			}
			return false;
		}
	}
}