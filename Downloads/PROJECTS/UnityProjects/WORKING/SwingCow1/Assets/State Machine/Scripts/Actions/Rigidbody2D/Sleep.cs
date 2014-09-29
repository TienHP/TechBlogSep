using UnityEngine;
using System.Collections;

namespace StateMachine.Action.URigidbody2D{
	[Info (category = "Rigidbody2D",    
	       description = "Make the rigidbody sleep.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Rigidbody2D.Sleep.html")]
	[System.Serializable]
	public class Sleep : Rigidbody2DAction {
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			rigidbody.Sleep();
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnFixedUpdate ()
		{
			rigidbody.Sleep();
		}
		
	}
}