using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Remove clip from the animation list.",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.RemoveClip.html")]
	[System.Serializable]
	public class RemoveClip : AnimationAction {
		[FieldInfo(tooltip="Name of the animation clip.")]
		public StringParameter _name;
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			animation.RemoveClip ( _name.Value);
		}
		
	}
}