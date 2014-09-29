﻿using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Physics",    
	       description = "True when the ray intersects any collider, otherwise false.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Physics.Raycast.html")]
	[System.Serializable]
	public class Raycast : StateCondition {
		[FieldInfo(tooltip="The starting point of the ray in world coordinates.")]
		public Vector3Parameter origin;
		[FieldInfo(tooltip="The direction of the ray.")]
		public Vector3Parameter direction;
		[FieldInfo(tooltip="The length of the ray.")]
		public FloatParameter distance;
		[FieldInfo(tooltip="Layer masks can be used selectively filter game objects for example when casting rays.")]
		public LayerMask layerMask;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;
		
		
		public override bool Validate ()
		{	
			return Physics.Raycast(origin.Value,direction.Value,distance.Value,layerMask) == equals.Value;
		}
	}
}