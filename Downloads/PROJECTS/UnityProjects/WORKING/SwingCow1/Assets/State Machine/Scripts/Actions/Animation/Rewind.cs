using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Rewinds the animation named name.",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.Rewind.html")]
	[System.Serializable]
	public class Rewind : AnimationAction {
		[FieldInfo(tooltip="Animation name.")]
		public StringParameter _name;
		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			animation.Rewind (_name.Value);
			
		}
		
	}
}