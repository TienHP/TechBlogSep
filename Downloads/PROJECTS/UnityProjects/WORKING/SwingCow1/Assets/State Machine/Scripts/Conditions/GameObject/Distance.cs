using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "GameObject",   
	       description = "Checks the distance between two game objects.",
	       url = "")]
	[System.Serializable]
	public class Distance : StateCondition {
		[FieldInfo(canBeConstant=false,requiredField=false, nullLabel="Owner",tooltip="First game object to test.")]
		public ObjectParameter first;
		[FieldInfo(canBeConstant=false, nullLabel="None",tooltip="Second game object to test.")]
		public ObjectParameter second;
		[FieldInfo(tooltip="Is the distance greater or less?")]
		public FloatComparer comparer;
		[FieldInfo(tooltip="Value to test with.")]
		public FloatParameter value;

		public override void OnEnter ()
		{
			if (first.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter "+first.Name +" in condition "+GetType().ToString()+" is null. Condition disabled!");
				return;
			}

			if (second.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter "+second.Name +" in condition "+GetType().ToString()+" is null. Condition disabled!");
				return;
			}

		}

		public override bool Validate ()
		{
			float distance = Vector3.Distance (((GameObject)first.Value).transform.position, ((GameObject)second.Value).transform.position);

			switch (comparer) {
			case FloatComparer.Less:
				return distance < value.Value;
			case FloatComparer.Greater:
				return distance > value.Value;
			}
			return false;
		}
	}
}