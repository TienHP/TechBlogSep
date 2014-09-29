using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Application",    
	       description = "Can the streamed level be loaded?",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Application.CanStreamedLevelBeLoaded.html")]
	[System.Serializable]
	public class CanStreamedLevelBeLoaded : StateAction {
		[FieldInfo(tooltip="The name of the level.")]
		public StringParameter level;
		[FieldInfo(canBeConstant=false, tooltip="Result to store.")]
		public BoolParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			if (string.IsNullOrEmpty (level.Value)) {
				disabled=true;
				Debug.Log("Could not execute "+ GetType().ToString()+", because the level name is empty. Action disabled!");
				return;
			}
			store.Value = Application.CanStreamedLevelBeLoaded (level.Value);	
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			store.Value = Application.CanStreamedLevelBeLoaded (level.Value);	
		}
	}
}