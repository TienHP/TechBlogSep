using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Vector/Vector3", 
	       description = "Subtracts one vector from another.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Vector3-operator_subtract.html")]
	[System.Serializable]
	public class Subtract : StateAction {
		[FieldInfo(tooltip="Vector3 value.")]
		public Vector3Parameter a;
		[FieldInfo(tooltip="Vector3 value to subtract.")]
		public Vector3Parameter b;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Store the result.")]
		public Vector3Parameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value = a.Value-b.Value;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = a.Value-b.Value;
		}
	}
}