using UnityEngine;
using System.Collections;

namespace StateMachine.Action.Prefs{
	[Info (category = "PlayerPrefs",   
	       description = "Sets the value of the preference identified by key.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/PlayerPrefs.SetString.html")]
	[System.Serializable]
	public class SetString : StateAction {
		[FieldInfo(tooltip="The key to set.")]
		public StringParameter key;
		[FieldInfo(tooltip="The value to set.")]
		public StringParameter value;
		
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			PlayerPrefs.SetString (key.Value, value.Value);
			if (!everyFrame) {
				Finish();			
			}
		}

		public override void OnUpdate ()
		{	
			PlayerPrefs.SetString (key.Value, value.Value);
		}
		
	}
}