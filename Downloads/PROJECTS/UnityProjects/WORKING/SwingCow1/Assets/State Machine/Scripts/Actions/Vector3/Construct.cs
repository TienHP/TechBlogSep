using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityVector3{
	[Info (category = "Vector/Vector3",    
	       description = "Constructs a new Vector3.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Vector3.html")]
	[System.Serializable]
	public class Construct : StateAction {
		[FieldInfo(tooltip="X component of the vector.")]
		public FloatParameter x;
		[FieldInfo(tooltip="Y component of the vector.")]
		public FloatParameter y;
		[FieldInfo(tooltip="Z component of the vector.")]
		public FloatParameter z;
		
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false, tooltip="Store the result.")]
		public Vector3Parameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value = new Vector3 (x.Value, y.Value, z.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = new Vector3 (x.Value, y.Value, z.Value);
		}
	}
}