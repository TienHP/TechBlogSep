using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Vector/Vector3",    
	       description = "Returns the length of this vector.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Vector3-magnitude.html")]
	[System.Serializable]
	public class Magnitude : StateAction {
		[FieldInfo(tooltip="Vector3 value.")]
		public Vector3Parameter vector;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Store the result.")]
		public FloatParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value = vector.Value.magnitude;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = vector.Value.magnitude;
		}
	}
}