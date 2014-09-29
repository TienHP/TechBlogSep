using UnityEngine;
using System.Collections;

namespace StateMachine.Action.URigidbody2D{
	[Info (category = "Rigidbody2D",    
	       description = "Disables the sleeping state of a rigidbody.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Rigidbody2D.WakeUp.html")]
	[System.Serializable]
	public class WakeUp : Rigidbody2DAction {
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			rigidbody.WakeUp();
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnFixedUpdate ()
		{
			rigidbody.WakeUp();
		}
		
	}
}