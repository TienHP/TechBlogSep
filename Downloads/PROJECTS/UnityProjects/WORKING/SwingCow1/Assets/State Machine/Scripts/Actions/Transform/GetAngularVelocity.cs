﻿using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityTransform{
	[Info (category = "Transform",   
	       description = "Get the angle between gameObject and target.",
	       url = "")]
	[System.Serializable]
	public class GetAngularVelocity : GameObjectAction {
		[FieldInfo(canBeConstant=false,nullLabel="None", tooltip="The Transform to use.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="")]
		public FloatParameter responseTime;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Store the result.")]
		public FloatParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			Vector3 dir = ((GameObject)target.Value).transform.position - ((GameObject)gameObject.Value).transform.position;
			dir.y = 0;
			float angle = FindAngle( ((GameObject)gameObject.Value).transform.forward, dir, ((GameObject)gameObject.Value).transform.up);
			float v = angle / responseTime.Value;
			store.Value=v;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			Vector3 dir = ((GameObject)target.Value).transform.position - ((GameObject)gameObject.Value).transform.position;
			dir.y = 0;
			float angle = FindAngle( ((GameObject)gameObject.Value).transform.forward, dir, ((GameObject)gameObject.Value).transform.up);
			float v = angle / responseTime.Value;
			store.Value=v;
		}
		
		private float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
		{
			// If the vector the angle is being calculated to is 0...
			if(toVector == Vector3.zero)
				// ... the angle between them is 0.
				return 0f;
			// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
			float angle = Vector3.Angle(fromVector, toVector);
			// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
			Vector3 normal = Vector3.Cross(fromVector, toVector);
			// The dot product of the normal with the upVector will be positive if they point in the same direction.
			angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
			// We need to convert the angle we've found from degrees to radians.
			angle *= Mathf.Deg2Rad;
			return angle;
		}
	}
}