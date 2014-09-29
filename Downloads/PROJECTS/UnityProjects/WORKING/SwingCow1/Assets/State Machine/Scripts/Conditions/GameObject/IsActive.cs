using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "GameObject",    
	       description = "The local active state of this GameObject.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/GameObject-activeSelf.html")]
	[System.Serializable]
	public class IsActive : StateCondition {
		[FieldInfo(canBeConstant=false,nullLabel="None",tooltip="GameObject to test.")]
		public ObjectParameter target;
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
			return ((GameObject)target.Value).activeSelf== equals.Value;
		}
	}
}