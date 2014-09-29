using UnityEngine;
using System.Collections;

namespace StateMachine.Action.URigidbody{
	[Info (category = "Rigidbody",    
	       description = "Applies force at position. As a result this will apply a torque and force on the object.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Rigidbody.AddForceAtPosition.html")]
	[System.Serializable]
	public class AddForceAtPosition : RigidbodyAction {
		[FieldInfo(tooltip="The force to add.")]
		public Vector3Parameter force;
		[FieldInfo(tooltip="Position to add the force at.")]
		public Vector3Parameter position;
		[FieldInfo(tooltip="Option for how to apply a force using Rigidbody.AddForce.")]
		public ForceMode mode;
		
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			rigidbody.AddForceAtPosition (force.Value, position.Value, mode);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnFixedUpdate ()
		{
			rigidbody.AddForceAtPosition (force.Value, position.Value, mode);
		}
		
	}
}