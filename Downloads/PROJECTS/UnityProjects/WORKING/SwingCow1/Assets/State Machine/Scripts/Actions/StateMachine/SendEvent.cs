using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "StateMachine",    
	       description = "Sends an event to the state machine. Can be checked in condition OnCustomEvent.",
	       url = "")]
	[System.Serializable]
	public class SendEvent : StateAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="The game object to use.")]
		public ObjectParameter gameObject;
		[FieldInfo(tooltip="Event name to send.")]
		public StringParameter _event;

		public override void OnEnter ()
		{
			if (gameObject.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter in action "+GetType().ToString()+" is null. If you assigned the parameter in the same state, create a new state, transition to it and execute this action. Action disabled!");
				return;
			}

			GameObject behaviorGameObject = (GameObject)gameObject.Value;
			// Find the correct behavior tree based on the grouping
			var behaviorComponents = behaviorGameObject.GetComponents<StateMachineBehaviour>();
			if (behaviorComponents != null && behaviorComponents.Length > 0) {
				for (int i = 0; i < behaviorComponents.Length; ++i) {
					behaviorComponents[i].SendEvent(_event.Value);
				}

			}
		}
	}
}