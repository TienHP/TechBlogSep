﻿using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "StateMachine",    
	       description = "",
	       url = "")]
	[System.Serializable]
	public class DisableStateMachine : StateAction {
		[FieldInfo(requiredField=false, canBeConstant=false,nullLabel="Owner", tooltip="The game object to use.")]
		public ObjectParameter gameObject;
		[FieldInfo(tooltip="The group of the StateMachineBehaviour.")]
		public IntParameter group;
		[FieldInfo(tooltip="Should the state machine be paused?")]
		public BoolParameter pause;
		private StateMachineBehaviour behaviour;
		
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
				behaviour = behaviorComponents[0];
				//  We don't need the behaviorTreeGroup if there is only one behavior tree component
				if (behaviorComponents.Length > 1) {
					for (int i = 0; i < behaviorComponents.Length; ++i) {
						if (behaviorComponents[i].group == group.Value) {
							// Cache the result when we have a match and stop looping.
							behaviour = behaviorComponents[i];
							break;
						}
					}
				}
			}
			if (behaviour != null) {
				behaviour.DisableStateMachine(pause.Value);			
			}
		}
	}
}