using UnityEngine;
using System.Collections;

namespace StateMachine.Action.URigidbody2D{
	[Info (category = "Rigidbody2D",    
	       description = "Adds a force to the rigidbody. As a result the rigidbody will start moving.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Rigidbody2D.AddRelativeForce.html")]
	[System.Serializable]
	public class AddRelativeForce : Rigidbody2DAction {
		[FieldInfo(tooltip="The force to add.")]
		public Vector2Parameter force;
		public ForceMode2D mode;
		
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			rigidbody.AddRelativeForce (force.Value,mode);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnFixedUpdate ()
		{
			rigidbody.AddRelativeForce (force.Value,mode);
		}
		
	}
}