using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public abstract class GameObjectAction : StateAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="The game object to use.")]
		public ObjectParameter gameObject;

		public override void OnEnter ()
		{
			if (gameObject.Value == null) {
				disabled=true;
				Debug.LogWarning("["+stateMachine.name+"]"+"GameObject paramter in action "+GetType().ToString()+" is null. If you assigned the parameter in the same state, create a new state, transition to it and execute this action. Action disabled!");
				return;
			}
			
		}
	}
}