using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UVector3{
	[Info (category = "Vector/Vector3", 
	       description = "Linearly interpolates between two vectors.",
	       url = "http://docs.unity3d.com/ScriptReference/Vector3.Lerp.html")]
	[System.Serializable]
	public class Lerp : StateAction {
		[FieldInfo(tooltip="")]
		public Vector3Parameter from;
		[FieldInfo(tooltip="")]
		public Vector3Parameter to;
		[FieldInfo(tooltip="")]
		public FloatParameter smooth;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Store the result.")]
		public Vector3Parameter store;

		public override void OnUpdate ()
		{
			store.Value = Vector3.Lerp (from.Value, to.Value, Time.deltaTime * smooth.Value);
		}
	}
}