using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "GameObject",    
	       description = "Checks if the game object's layer is equal to the test layer.",
	       url = "")]
	[System.Serializable]
	public class IsLayer : StateCondition {
		[FieldInfo(canBeConstant=false,nullLabel="None",tooltip="GameObject to test.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="The string to test with.")]
		public IntParameter layer;
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
			return (((GameObject)target.Value).layer == layer.Value)== equals.Value;
		}
	}
}