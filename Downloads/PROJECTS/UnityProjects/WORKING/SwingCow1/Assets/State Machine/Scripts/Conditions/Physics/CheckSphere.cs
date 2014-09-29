using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Physics",  
	       description = "True if there are any colliders overlapping the sphere defined by position and radius in world coordinates.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics.CheckSphere.html")]
	[System.Serializable]
	public class CheckSphere : StateCondition {
		[FieldInfo(tooltip="Position.")]
		public Vector3Parameter position;
		[FieldInfo(tooltip="Radius of the sphere")]
		public FloatParameter radius;
		[FieldInfo(tooltip="Layer masks can be used selectively filter game objects for example when casting rays.")]
		public LayerMask layerMask;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;
		
		
		public override bool Validate ()
		{	
			return Physics.CheckSphere(position.Value,radius.Value,layerMask) == equals.Value;
		}
	}
}