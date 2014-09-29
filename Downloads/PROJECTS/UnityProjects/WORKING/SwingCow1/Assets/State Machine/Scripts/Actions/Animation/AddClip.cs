using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Adds a clip to the animation with name.",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.AddClip.html")]
	[System.Serializable]
	public class AddClip : AnimationAction {
		[FieldInfo(tooltip="Clip to add.")]
		public ObjectParameter clip;
		[FieldInfo(tooltip="New name.")]
		public StringParameter _name;
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			animation.AddClip ((AnimationClip)clip.Value, _name.Value);
		}

	}
}