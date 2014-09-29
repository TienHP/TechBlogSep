using UnityEngine;
using System.Collections;

namespace StateMachine.Action.URigidbody2D{
	[Info (category = "Rigidbody2D",    
	       description = "Apply a torque at the rigidbody's centre of mass.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Rigidbody2D.AddTorque.html")]
	[System.Serializable]
	public class AddTorque : Rigidbody2DAction {
		[FieldInfo(tooltip="The torque to add.")]
		public FloatParameter torque;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			rigidbody.AddTorque (torque.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnFixedUpdate ()
		{
			rigidbody.AddTorque (torque.Value);
		}
		
	}
}