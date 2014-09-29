using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Application",   
	       description = "Is the current platform WebPlayer?",
	       url = "")]
	[System.Serializable]
	public class IsWebPlayer : StateCondition {
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;
		
		public override bool Validate ()
		{
			return Application.isWebPlayer==equals.Value;
		}
		
	}
}