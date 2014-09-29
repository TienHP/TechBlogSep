using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Wrapping mode of the animation.",
	       url = "http://docs.unity3d.com/ScriptReference/AnimationState-wrapMode.html")]
	[System.Serializable]
	public class SetWrapMode : AnimationAction {
		[FieldInfo(tooltip="Animation name.")]
		public StringParameter _name;
		public WrapMode wrapMode;

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			animation [_name.Value].wrapMode = wrapMode;
		}
		
	}
}