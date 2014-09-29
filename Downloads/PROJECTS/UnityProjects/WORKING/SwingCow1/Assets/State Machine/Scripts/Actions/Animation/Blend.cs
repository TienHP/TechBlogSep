using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Blends the animation towards targetWeight over the next time seconds.",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.Blend.html")]
	[System.Serializable]
	public class Blend : AnimationAction {
		[FieldInfo(tooltip="Animation name to blend.")]
		public StringParameter anim;
		[FieldInfo(tooltip="Weight",defaultValue=1.0f)]
		public FloatParameter targetWeight;
		[FieldInfo(tooltip="Fading time.",defaultValue=0.3f)]
		public FloatParameter fadeLenght;

		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			animation.Blend (anim.Value,targetWeight.Value, fadeLenght.Value);
			if (!everyFrame) {
				Finish();			
			}
		}
		
		public override void OnUpdate ()
		{
			animation.Blend (anim.Value,targetWeight.Value, fadeLenght.Value);
		}
	}
}