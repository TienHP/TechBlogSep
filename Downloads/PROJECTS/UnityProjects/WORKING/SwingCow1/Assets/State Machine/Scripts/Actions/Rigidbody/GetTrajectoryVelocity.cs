using UnityEngine;
using System.Collections;

namespace StateMachine.Action.URigidbody{
	[Info (category = "Rigidbody",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class GetTrajectoryVelocity : StateAction {
		[FieldInfo(tooltip="Start position.")]
		public Vector3Parameter start;
		[FieldInfo(tooltip="End position.")]
		public Vector3Parameter target;
		[FieldInfo(tooltip="Multiplier")]
		public FloatParameter lob;
		[FieldInfo(tooltip="Gravity")]
		public Vector3Parameter gravity;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Store the result.")]
		public Vector3Parameter store;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;
		
		public override void OnUpdate ()
		{

			store.Value = GetTrajectory (start.Value, target.Value, lob.Value, gravity.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public Vector3 GetTrajectory(Vector3 startingPosition, Vector3 targetPosition, float lob, Vector3 gravity)
		{
			float physicsTimestep = Time.fixedDeltaTime;
			float timestepsPerSecond = Mathf.Ceil(1f/physicsTimestep);
			
			//By default we set n so our projectile will reach our target point in 1 second
			float n = lob * timestepsPerSecond;
			
			Vector3 a = physicsTimestep * physicsTimestep * gravity;
			Vector3 p = targetPosition;
			Vector3 s = startingPosition;
			
			Vector3 velocity = (s + (((n * n + n) * a) / 2f) - p) * -1 / n;
			
			//This will give us velocity per timestep. The physics engine expects velocity in terms of meters per second
			velocity /= physicsTimestep;
			return velocity;
		}
	}
}