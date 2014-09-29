using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Cross fades an animation after previous animations has finished playing.",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.CrossFadeQueued.html")]
	[System.Serializable]
	public class CrossFadeQueued : AnimationAction {
		[FieldInfo(tooltip="Animation name to crossfade to.")]
		public StringParameter anim;
		[FieldInfo(tooltip="Fading time.",defaultValue=0.3f)]
		public FloatParameter fadeLenght;
		[FieldInfo(tooltip="QueueMode.")]
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
			animation.CrossFadeQueued (anim.Value, fadeLenght.Value,queue,mode);
			if (!everyFrame) {
				Finish();			
			}
		}
		
		public override void OnUpdate ()
		{
			animation.CrossFadeQueued (anim.Value, fadeLenght.Value, queue, mode);
		}
	}
}