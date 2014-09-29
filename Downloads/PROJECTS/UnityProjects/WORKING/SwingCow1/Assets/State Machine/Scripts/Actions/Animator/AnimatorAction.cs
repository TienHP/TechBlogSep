using UnityEngine;
using System.Collections;

namespace StateMachine.Action.UnityAnimator{
	[Info (category = "",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public abstract class AnimatorAction : StateAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="The animator to use.")]
		public ObjectParameter gameObject;
		
		protected Animator animator;
		
		public override void OnEnter (){
			if (gameObject.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter in action "+GetType().ToString()+" is null. If you assigned the parameter in the same state, create a new state, transition to it and execute this action. Action disabled!");
				return;
			}

			animator = ((GameObject)gameObject.Value).GetComponent<Animator> ();
			if (animator == null) {
				disabled=true;
				Debug.LogWarning("Missing Component! "+ GetType().ToString()+ " requires the Animator component on the GameObject. Action disabled! If you added the component in the same state, create a new state to run this action.");
			}
		}
	}
}