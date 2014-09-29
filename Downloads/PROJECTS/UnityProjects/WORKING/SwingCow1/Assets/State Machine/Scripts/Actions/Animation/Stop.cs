using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "Animation",   
	       description = "Stops all playing animations that were started with this Animation.",
	       url = "http://docs.unity3d.com/ScriptReference/Animation.Stop.html")]
	[System.Serializable]
	public class Stop : AnimationAction {

		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			animation.Stop ();

		}

	}
}