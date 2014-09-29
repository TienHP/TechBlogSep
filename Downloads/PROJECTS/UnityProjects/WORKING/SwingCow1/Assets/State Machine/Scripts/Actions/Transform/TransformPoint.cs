using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Transform",   
	       description = "Transforms position from local space to world space.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Transform.TransformPoint.html")]
	[System.Serializable]
	public class TransformPoint : GameObjectAction {
		[FieldInfo(tooltip="Position that will be transformed.")]
		public Vector3Parameter position;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Store the result point.")]
		public Vector3Parameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			store.Value = ((GameObject)gameObject.Value).transform.TransformPoint (position.Value);
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = ((GameObject)gameObject.Value).transform.TransformPoint (position.Value);
		}
	}
}