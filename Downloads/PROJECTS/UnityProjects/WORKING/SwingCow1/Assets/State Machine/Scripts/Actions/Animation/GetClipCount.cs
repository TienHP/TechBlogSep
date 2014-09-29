using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Get the number of clips currently assigned to this animation.",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.GetClipCount.html")]
	[System.Serializable]
	public class GetClipCount : AnimationAction {
		[FieldInfo(tooltip="Store the result.")]
		public IntParameter store;
	
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			store.Value = animation.GetClipCount ();
			if (!everyFrame) {
				Finish();			
			}
		}
		
		public override void OnUpdate ()
		{
			store.Value = animation.GetClipCount ();
		}
	}
}