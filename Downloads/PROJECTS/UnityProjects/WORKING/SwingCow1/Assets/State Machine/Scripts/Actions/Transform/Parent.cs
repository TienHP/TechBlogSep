using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Transform",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class Parent : GameObjectAction {
		[FieldInfo(canBeConstant=false,nullLabel="None", tooltip="Target to parent.")]
		public ObjectParameter target;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			Transform gameObjectTransform = ((GameObject)gameObject.Value).transform;
			Transform targetTransform = ((GameObject)target.Value).transform;
			targetTransform.parent = gameObjectTransform;
			
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			Transform gameObjectTransform = ((GameObject)gameObject.Value).transform;
			Transform targetTransform = ((GameObject)target.Value).transform;
			targetTransform.parent = gameObjectTransform;
		}
	}
}