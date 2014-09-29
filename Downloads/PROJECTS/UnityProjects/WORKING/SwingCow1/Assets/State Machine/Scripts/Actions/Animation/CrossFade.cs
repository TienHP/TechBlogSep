using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Fades the animation with name animation in over a period of time seconds and fades other animations out.",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.CrossFade.html")]
	[System.Serializable]
	public class CrossFade : AnimationAction {
		[FieldInfo(tooltip="Animation name to crossfade to.")]
		public StringParameter anim;
		[FieldInfo(tooltip="Fading time.",defaultValue=0.3f)]
		public FloatParameter fadeLenght;
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
			animation.CrossFade (anim.Value, fadeLenght.Value, mode);
			if (!everyFrame) {
				Finish();			
			}
		}
		
		public override void OnUpdate ()
		{
			animation.CrossFade (anim.Value, fadeLenght.Value, mode);
		}
	}
}