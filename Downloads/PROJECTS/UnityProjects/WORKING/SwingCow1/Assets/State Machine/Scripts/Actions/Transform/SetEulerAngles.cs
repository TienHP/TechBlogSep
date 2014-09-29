using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Transform",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public class SetEulerAngles : GameObjectAction {
		[FieldInfo(tooltip="Euler angles to set.")]
		public Vector3Parameter eulerAngles;
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
			((GameObject)gameObject.Value).transform.rotation=Quaternion.Lerp(((GameObject)gameObject.Value).transform.rotation, Quaternion.Euler(eulerAngles.Value),Time.deltaTime*smooth.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			((GameObject)gameObject.Value).transform.rotation=Quaternion.Lerp(((GameObject)gameObject.Value).transform.rotation, Quaternion.Euler(eulerAngles.Value),Time.deltaTime*smooth.Value);
		}
	}
}