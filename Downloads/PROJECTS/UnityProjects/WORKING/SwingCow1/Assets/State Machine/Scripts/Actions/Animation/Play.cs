using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Plays animation without any blending.",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.Play.html")]
	[System.Serializable]
	public class Play : AnimationAction {
		[FieldInfo(tooltip="Animation name to crossfade to.")]
		public StringParameter anim;
		[FieldInfo(tooltip="PlayMode.")]
		public PlayMode mode=PlayMode.StopSameLayer;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			animation.Play (anim.Value, mode);
			if (!everyFrame) {
				Finish();			
			}
		}
		public override void OnUpdate ()
		{
			animation.Play (anim.Value, mode);
		}
	}
}