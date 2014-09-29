using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Transform",   
	       description = "Set the position of the transform in world space.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform-position.html")]
	[System.Serializable]
	public class SetPosition : GameObjectAction {
		[FieldInfo(tooltip="Position to set.")]
		public Vector3Parameter position;
		[FieldInfo(tooltip="Smooth multiplier.")]
		public FloatParameter smooth;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			if (smooth.Value == 0) {
				((GameObject)gameObject.Value).transform.position=position.Value;		
			} else {
				((GameObject)gameObject.Value).transform.position = Vector3.Lerp (((GameObject)gameObject.Value).transform.position, position.Value, Time.deltaTime * smooth.Value);		
			}
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			if (smooth.Value == 0) {
				((GameObject)gameObject.Value).transform.position=position.Value;		
			} else {
				((GameObject)gameObject.Value).transform.position = Vector3.Lerp (((GameObject)gameObject.Value).transform.position, position.Value, Time.deltaTime * smooth.Value);		
			}
		}
	}
}