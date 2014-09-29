using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Transform",  
	       description = "The green axis of the transform in world space.",
	       url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform-up.html")]
	[System.Serializable]
	public class Up : GameObjectAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Store the result.")]
		public Vector3Parameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			store.Value = ((GameObject)gameObject.Value).transform.up;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = ((GameObject)gameObject.Value).transform.up;
		}
	}
}