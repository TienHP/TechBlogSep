using UnityEngine;
using System.Collections;

namespace StateMachine.Action.URigidbody2D{
	[Info (category = "Rigidbody2D",    
	       description = "Apply a force at a given position in space.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Rigidbody2D.AddForceAtPosition.html")]
	[System.Serializable]
	public class AddForceAtPosition : Rigidbody2DAction {
		[FieldInfo(tooltip="The force to add.")]
		public Vector2Parameter force;
		[FieldInfo(tooltip="Position to add the force at.")]
		public Vector2Parameter position;
	
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			rigidbody.AddForceAtPosition (force.Value, position.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnFixedUpdate ()
		{
			rigidbody.AddForceAtPosition (force.Value, position.Value);
		}
		
	}
}