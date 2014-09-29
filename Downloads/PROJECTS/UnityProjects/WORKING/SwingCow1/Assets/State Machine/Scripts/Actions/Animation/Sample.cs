using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Samples animations at the current state.",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.Sample.html")]
	[System.Serializable]
	public class Sample : AnimationAction {
		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			animation.Sample ();
			
		}
		
	}
}