using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "PlayerPrefs",   
	       description = "Stores true if key exists in the preferences.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/PlayerPrefs.HasKey.html")]
	[System.Serializable]
	public class HasKey : StateAction {
		[FieldInfo(tooltip="The key to get.")]
		public StringParameter key;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false,nullLabel="None", tooltip="Store the result.")]
		public BoolParameter store;
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			store.Value = PlayerPrefs.HasKey (key.Value);
			if (!everyFrame) {
				Finish();			
			}
		}

		public override void OnUpdate ()
		{	
			store.Value = PlayerPrefs.HasKey (key.Value);
		}
		
	}
}