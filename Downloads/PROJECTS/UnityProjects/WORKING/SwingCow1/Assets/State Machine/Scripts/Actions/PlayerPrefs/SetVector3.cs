using UnityEngine;
using System.Collections;

namespace StateMachine.Action.Prefs{
	[Info (category = "PlayerPrefs",   
	       description = "Sets the value of the preference identified by key.",
	       url = "")]
	[System.Serializable]
	public class SetVector3 : StateAction {
		[FieldInfo(tooltip="The key to set.")]
		public StringParameter key;
		[FieldInfo(tooltip="The value to set.")]
		public Vector3Parameter value;
		
		[FieldInfo(tooltip="Execute the action every frame.")]
		public bool everyFrame;

		public override void OnEnter ()
		{
			base.OnEnter ();
			PlayerPrefs.SetFloat (key.Value+"_x", value.Value.x);
			PlayerPrefs.SetFloat (key.Value+"_y", value.Value.y);
			PlayerPrefs.SetFloat (key.Value+"_z", value.Value.z);
			if (!everyFrame) {
				Finish();			
			}
		}

		public override void OnUpdate ()
		{	
			PlayerPrefs.SetFloat (key.Value+"_x", value.Value.x);
			PlayerPrefs.SetFloat (key.Value+"_y", value.Value.y);
			PlayerPrefs.SetFloat (key.Value+"_z", value.Value.z);
		}
		
	}
}