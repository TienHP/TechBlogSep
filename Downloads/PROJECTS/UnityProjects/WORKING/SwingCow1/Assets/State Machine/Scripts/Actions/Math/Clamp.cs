using UnityEngine;
using System.Collections;

namespace StateMachine.Action.Math{
	[Info (category = "Math", 
	       description = ".",
	       url = "")]
	[System.Serializable]
	public class Clamp : StateAction {
		[FieldInfo(tooltip="The value to clamp")]
		public FloatParameter value;
		[FieldInfo(tooltip="Min value")]
		public FloatParameter min;
		[FieldInfo(tooltip="Max value")]
		public FloatParameter max;

		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Store the result.")]
		public FloatParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value = Mathf.Clamp (value.Value, min.Value, max.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = Mathf.Clamp (value.Value, min.Value, max.Value);
		}
	}
}