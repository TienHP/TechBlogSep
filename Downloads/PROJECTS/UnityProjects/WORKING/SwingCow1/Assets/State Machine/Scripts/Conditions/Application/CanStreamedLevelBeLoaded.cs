﻿using UnityEngine;
using System.Collections;

namespace StateMachine.Condition{
	[Info (category = "Application",  
	       description = "Can the streamed level be loaded?",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Application.CanStreamedLevelBeLoaded.html")]
	[System.Serializable]
	public class CanStreamedLevelBeLoaded : StateCondition {
		[FieldInfo(tooltip="The name of the level.")]
		public StringParameter level;
		[FieldInfo(tooltip="Does the result equals this condition.")]
		public BoolParameter equals;

		public override void OnEnter ()
		{
			if (string.IsNullOrEmpty (level.Value)) {
				disabled=true;
				Debug.Log("Could not execute "+ GetType().ToString()+", because the level name is empty. Condition disabled!");
			}
		}

		public override bool Validate ()
		{
			return Application.CanStreamedLevelBeLoaded (level.Value)== equals.Value;	
		}
	}
}