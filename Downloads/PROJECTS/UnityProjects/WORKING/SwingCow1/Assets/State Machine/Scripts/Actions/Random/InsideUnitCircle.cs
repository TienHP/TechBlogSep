using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Random",   
	       description = "Random point inside a circle with radius.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Random-insideUnitCircle.html")]
	[System.Serializable]
	public class InsideUnitCircle : StateAction {
		[FieldInfo(tooltip="Radius of the circle.")]
		public FloatParameter radius;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Store the result.")]
		public Vector2Parameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value = Random.insideUnitCircle * radius.Value;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = Random.insideUnitCircle * radius.Value;
		}
	}
}