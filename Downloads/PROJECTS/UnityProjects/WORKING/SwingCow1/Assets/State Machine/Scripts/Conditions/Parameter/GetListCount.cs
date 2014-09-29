using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Parameter",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public class GetListCount : StateCondition {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Parameter to test.")]
		public ListParameter parameter;
		
		[FieldInfo(tooltip="Is the parameter greater or less?")]
		public FloatComparer comparer;
		[FieldInfo(tooltip="Value to test with.")]
		public IntParameter value;
		
		public override bool Validate ()
		{
			
			switch (comparer) {
			case FloatComparer.Less:
				return parameter.Value.Count < value.Value;
			case FloatComparer.Greater:
				return parameter.Value.Count > value.Value;
			}
			return false;
		}
	}
}