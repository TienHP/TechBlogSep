using UnityEngine;
using System.Collections;

namespace StateMachine.Action{
	[Info (category = "GameObject",  
	       description = "Calls the method named methodName on every MonoBehaviour in this game object or any of its children.",
	       url = "https://docs.unity3d.com/Documentation/ScriptReference/GameObject.BroadcastMessage.html")]
	[System.Serializable]
	public class BroadcastMessage : GameObjectAction {
		[FieldInfo(tooltip="The name of the method to call.")]
		public StringParameter methodName;
		[FieldInfo(tooltip="Should an error be raised if the method doesn't exist on the target object?")]
		public SendMessageOptions options;
		
		public override void OnEnter ()
		{
			base.OnEnter ();
			if (disabled) {
				return;			
			}
			if (string.IsNullOrEmpty (methodName.Value)) {
				disabled=true;
				Debug.Log("Could not execute "+ GetType().ToString()+", because the methodName parameter is empty. Action disabled!");
				return;
			}
			((GameObject)gameObject.Value).BroadcastMessage (methodName.Value, options);
			Finish ();
		}
	}
}