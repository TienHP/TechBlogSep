using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Input",   
	       description = "Number of touches. Guaranteed not to change throughout the frame. ",
	       url = "http://docs.unity3d.com/ScriptReference/Input-touchCount.html")]
	[System.Serializable]
	public class GetTouchCount : StateAction {
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false, tooltip="Store the result.")]
		public IntParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;
		
		public override void OnEnter ()
		{
			store.Value = Input.touchCount;
			if (!everyFrame) {
				Finish ();
			}
		}
		
		public override void OnUpdate ()
		{
			store.Value = Input.touchCount;	
		}
	}
}