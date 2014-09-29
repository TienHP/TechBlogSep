using UnityEngine;
using System.Collections;

namespace StateMachine.Action.Math{
	[Info (category = "Math",   
	       description = "Interpolates between a and b.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Mathf.Lerp.html")]
	[System.Serializable]
	public class Lerp : StateAction {
		[FieldInfo(tooltip="Interpolate from.")]
		public FloatParameter from;
		[FieldInfo(tooltip="Interpolate to.")]
		public FloatParameter to;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Store the result.")]
		public FloatParameter store;
	
		public override void OnUpdate ()
		{
			store.Value = Mathf.Lerp (from.Value, to.Value, Time.deltaTime);
		}
	}
}