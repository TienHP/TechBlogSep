using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "Input",    
	       description = "Returns true during the frame the user releases the key identified by name.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/Input.GetKeyUp.html")]
	[System.Serializable]
	public class GetKeyUp : StateAction {
		[FieldInfo(tooltip="Key name.")]
		public StringParameter keyName;
		[FieldInfo(canBeConstant=false,bindedCanBeConstant=false, tooltip="Store the result.")]
		public BoolParameter store;

		public override void OnEnter ()
		{
			if (string.IsNullOrEmpty (keyName.Value)) {
				disabled=true;
				Debug.Log("Could not execute "+ GetType().ToString()+", because the keyName parameter is empty. Action disabled!");
				return;
			}
			store.Value = Input.GetKeyUp (keyName.Value);	
		}

		public override void OnUpdate ()
		{
			store.Value = Input.GetKeyUp (keyName.Value);	
		}
	}
}