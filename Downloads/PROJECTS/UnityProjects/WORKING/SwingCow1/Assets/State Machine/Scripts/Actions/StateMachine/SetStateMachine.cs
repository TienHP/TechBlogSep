using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "StateMachine",    
	       description = "Switch to another state machine.",
	       url = "")]
	[System.Serializable]
	public class SetStateMachine : StateAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="The game object to use.")]
		public ObjectParameter gameObject;
		[FieldInfo(tooltip="State Machine to set.")]
		public ObjectParameter _stateMachine;

		public override void OnEnter ()
		{
			if (gameObject.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter in action "+GetType().ToString()+" is null. If you assigned the parameter in the same state, create a new state, transition to it and execute this action. Action disabled!");
				return;
			}

			StateMachineBehaviour behaviour= ((GameObject)gameObject.Value).GetComponent<StateMachineBehaviour> ();
			behaviour.SetStateMachine ((StateMachine)_stateMachine.Value);
		}
	}
}