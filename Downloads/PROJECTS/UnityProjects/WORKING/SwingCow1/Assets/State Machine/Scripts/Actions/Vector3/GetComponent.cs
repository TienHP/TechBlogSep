using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityVector3{
	[Info (category = "Vector/Vector3",    
	       description = ".",
	       url = "")]
	[System.Serializable]
	public class GetComponent : StateAction {
		[FieldInfo(tooltip="Vector to use.")]
		public Vector3Parameter vector;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false,nullLabel="Don't Use", tooltip="X component of the vector.")]
		public FloatParameter x;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,requiredField=false,nullLabel="Don't Use",tooltip="Y component of the vector.")]
		public FloatParameter y;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false, requiredField=false,nullLabel="Don't Use",tooltip="Z component of the vector.")]
		public FloatParameter z;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			x.Value = vector.Value.x;
			y.Value = vector.Value.y;
			z.Value = vector.Value.z;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			x.Value = vector.Value.x;
			y.Value = vector.Value.y;
			z.Value = vector.Value.z;
		}
	}
}