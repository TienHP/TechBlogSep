using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "StateMachine/State",   
	       description = "Deprecated, do not use. There is no need for this condition anymore.",
	       url = "")]
	[System.Serializable]
	public class IsFinished : StateCondition {
		//[FieldInfo(tooltip="Does the result equals this condition.")]
		//public BoolParameter equals;
		
		public override bool Validate ()
		{
			return true;//stateMachine.behaviour.currentState.IsFinished == equals.Value;
		}
	}
}