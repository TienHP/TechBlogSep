using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Plays an animation after previous animations has finished playing.",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.PlayQueued.html")]
	[System.Serializable]
	public class PlayQueued : AnimationAction {
		[FieldInfo(tooltip="Animation name to crossfade to.")]
		public StringParameter anim;
		[FieldInfo(tooltip="PlayMode.")]
		public QueueMode queue=QueueMode.CompleteOthers;
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
			animation.PlayQueued (anim.Value,queue, mode);
			if (!everyFrame) {
				Finish();			
			}
		}
		public override void OnUpdate ()
		{
			animation.PlayQueued (anim.Value,queue, mode);
		}
	}
}