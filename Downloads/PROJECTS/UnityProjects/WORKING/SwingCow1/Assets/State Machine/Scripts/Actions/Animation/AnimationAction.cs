using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimation{
	[Info (category = "",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public abstract class AnimationAction : StateAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="GameObject to use.")]
		public ObjectParameter gameObject;
		
		protected Animation animation;
		
		public override void OnEnter (){
			if (gameObject.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter in action "+GetType().ToString()+" is null. If you assigned the parameter in the same state, create a new state, transition to it and execute this action. Action disabled!");
				return;
			}
			
			animation = ((GameObject)gameObject.Value).animation;
			if (animation == null) {
				disabled=true;
				Debug.LogWarning("Missing Component! "+ GetType().ToString()+ " requires the Animation component on the GameObject. Action disabled! If you added the component in the same state, create a new state to run this action.");
			}
		}
	}
}