using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Transform",  
	       description = "Transforms direction from local space to world space.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Transform.TransformDirection.html")]
	[System.Serializable]
	public class TransformDirection : GameObjectAction {
		[FieldInfo(tooltip="Direction that will be transformed.")]
		public Vector3Parameter direction;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Store the result direction")]
		public Vector3Parameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			store.Value = ((GameObject)gameObject.Value).transform.TransformDirection (direction.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = ((GameObject)gameObject.Value).transform.TransformDirection (direction.Value);
		}
	}
}