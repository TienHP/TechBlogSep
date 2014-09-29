using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Parameter",    
	       description = "Sets the value of a global parameter.",
	       url = "")]
	[System.Serializable]
	public class SetVector3 : StateAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="The parameter to use.")]
		public Vector3Parameter parameter;
		[FieldInfo(tooltip="The value to set.")]
		public Vector3Parameter value;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			parameter.Value = value.Value;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			parameter.Value = value.Value;
		}
	}
}