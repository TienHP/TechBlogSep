﻿using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "",   
	       description = "",
	       url = "")]
	[System.Serializable]
	public abstract class AudioSourceAction : StateAction {
		[FieldInfo(requiredField=false,canBeConstant=false,nullLabel="Owner", tooltip="A game object that has an audio source in it.")]
		public ObjectParameter gameObject;
		protected AudioSource audio;
		
		public override void OnEnter ()
		{
			if (gameObject.Value == null) {
				disabled=true;
				Debug.LogWarning("GameObject paramter in action "+GetType().ToString()+" is null. If you assigned the parameter in the same state, create a new state, transition to it and execute this action. Action disabled!");
				return;
			}
			audio = ((GameObject)gameObject.Value).GetComponent<AudioSource> ();
			if (audio == null) {
				disabled=true;
				Debug.LogWarning("Missing Component! "+ GetType().ToString()+ " requires the AudioSource component on the GameObject. Action disabled!If you added the component in the same state, create a new state to run this action.");
			}
		}
	}
}