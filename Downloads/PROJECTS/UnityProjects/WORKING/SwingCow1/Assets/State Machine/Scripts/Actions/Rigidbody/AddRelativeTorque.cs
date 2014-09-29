﻿using UnityEngine;
using System.Collections;

namespace StateMachine.Action.URigidbody{
	[Info (category = "Rigidbody",    
	       description = "Adds a torque to the rigidbody relative to the rigidbodie's own coordinate system.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Rigidbody.AddRelativeTorque.html")]
	[System.Serializable]
	public class AddRelativeTorque : RigidbodyAction {
		[FieldInfo(tooltip="The torque to add.")]
		public Vector3Parameter torque;
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
			rigidbody.AddRelativeTorque (torque.Value, mode);
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnFixedUpdate ()
		{
			rigidbody.AddRelativeTorque (torque.Value, mode);
		}
		
	}
}