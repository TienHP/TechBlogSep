using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Transform",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class GetChildCount : GameObjectAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None",tooltip="Store the result.")]
		public IntParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			store.Value = ((GameObject)gameObject.Value).transform.childCount;
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = ((GameObject)gameObject.Value).transform.childCount;
		}
	}
}