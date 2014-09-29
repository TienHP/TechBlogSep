using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "PlayerPrefs",   
	       description = "Stores the value corresponding to key in the preference file if it exists.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/PlayerPrefs.GetString.html")]
	[System.Serializable]
	public class GetString : StateAction {
		[FieldInfo(tooltip="The key to get.")]
		public StringParameter key;
		[FieldInfo(tooltip="The default value to set, if the key does not exist.")]
		public StringParameter defaultValue;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Store the result.")]
		public StringParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value = PlayerPrefs.GetString (key.Value, defaultValue.Value);
			if (!everyFrame) {
				Finish();			
			}
		}

		public override void OnUpdate ()
		{	
			store.Value = PlayerPrefs.GetString (key.Value, defaultValue.Value);
		}
		
	}
}