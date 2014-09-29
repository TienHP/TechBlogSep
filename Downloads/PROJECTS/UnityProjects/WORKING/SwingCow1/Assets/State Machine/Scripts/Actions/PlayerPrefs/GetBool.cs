using UnityEngine;
using System.Collections;

namespace StateMachine.Action.Prefs{
	[Info (category = "PlayerPrefs",   
	       description = "Stores the value corresponding to key in the preference file if it exists.",
	       url = "")]
	[System.Serializable]
	public class GetBool : StateAction {
		[FieldInfo(tooltip="The key to get.")]
		public StringParameter key;
		[FieldInfo(tooltip="The default value to set, if the key does not exist.")]
		public BoolParameter defaultValue;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Store the result")]
		public BoolParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value = (PlayerPrefs.GetInt (key.Value, defaultValue.Value?1:0)==0?false:true);
			
			if (!everyFrame) {
				Finish();			
			}
		}

		public override void OnUpdate ()
		{	
			store.Value = (PlayerPrefs.GetInt (key.Value, defaultValue.Value?1:0)==0?false:true);
		}
		
	}
}