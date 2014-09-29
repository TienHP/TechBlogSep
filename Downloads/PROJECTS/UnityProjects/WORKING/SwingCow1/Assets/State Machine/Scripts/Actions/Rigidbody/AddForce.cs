using UnityEngine;
using System.Collections;

namespace StateMachine.Action.URigidbody{
	[Info (category = "Rigidbody",    
	       description = "Adds a force to the rigidbody. As a result the rigidbody will start moving.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Rigidbody.AddForce.html")]
	[System.Serializable]
	public class AddForce : RigidbodyAction {
		[FieldInfo(tooltip="The force to add.")]
		public Vector3Parameter force;
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
			rigidbody.AddForce (force.Value, mode);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnFixedUpdate ()
		{
			rigidbody.AddForce (force.Value, mode);
		}

	}
}