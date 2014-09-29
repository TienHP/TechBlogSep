using UnityEngine;
using System.Collections;

namespace StateMachine.Action.URigidbody2D{
	[Info (category = "Rigidbody2D",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class SetVelocity : Rigidbody2DAction {
		[FieldInfo(tooltip="The force to add.")]
		public Vector2Parameter velocity;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			rigidbody.velocity = velocity.Value;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnFixedUpdate ()
		{
			rigidbody.velocity = velocity.Value;
		}
		
	}
}